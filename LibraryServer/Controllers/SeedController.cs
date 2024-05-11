using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryServer;
using CsvHelper.Configuration;
using System.Globalization;
using CsvHelper;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using LibraryServer.LibraryModel;
using LibraryServer.Data;
namespace LibraryServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedController(LibraryGoldenContext db, IHostEnvironment environment,
        UserManager<LibraryUser> userManager) : ControllerBase
    {
        private readonly string _bookPathName = Path.Combine(environment.ContentRootPath, "Data/librarybooks.csv");
        private readonly string _patronPathName = Path.Combine(environment.ContentRootPath, "Data/librarypatrons.csv");
        private readonly LibraryGoldenContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<LibraryUser> _userManager;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;

        [HttpPost("User")]
        public async Task<ActionResult> SeedUsers()
        {
            (string name, string email) = ("user1", "user1@yahoo.com");
            LibraryUser user = new()
            {
                UserName = name,
                Email = email,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            if (await userManager.FindByNameAsync(name) is not null)
            {
                user.UserName = "user2";
            }
            _ = await userManager.CreateAsync(user, "P@ssw0rd!")
                ?? throw new InvalidOperationException();
            user.EmailConfirmed = true;
            user.LockoutEnabled = false;
            await db.SaveChangesAsync();

            return Ok();

        }

        [HttpPost("Patron")]
        public async Task<ActionResult<Patron>> SeedPatron()
        {
            Dictionary<string, Patron> patrons = await db.Patrons//.AsNoTracking()
            .ToDictionaryAsync(p => p.PatronFname);

            CsvConfiguration config = new(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                HeaderValidated = null
            };
            int patronCount = 0;
            using (StreamReader reader = new(_patronPathName))
            using (CsvReader csv = new(reader, config))
            {
                IEnumerable<PatronCSV>? records = csv.GetRecords<PatronCSV>();
                foreach (PatronCSV record in records)
                {
                    Patron patron = new()
                    {
                        PatronFname = record.PatronFName,
                        PatronLname = record.PatronLName,
                        PatronAddress = record.PatronAddress,
                        PatronCheckedBookId = record.PatronCheckedBookID,
                        PatronCheckedDate = record.PatronCheckedDate,
                        PatronCheckedDueDate = record.PatronCheckedDueDate,
                        PatronCheckedOverdueAmt = record.PatronCheckedOverdueAmt,
                    };
                    db.Patrons.Add(patron);
                    patronCount++;
                }
                await db.SaveChangesAsync();
            }
            return new JsonResult(patronCount);
        }

        [HttpPost("Book")]
        public async Task<ActionResult<Book>> SeedBook()
        {
            // create a lookup dictionary containing all the books already existing 
            // into the Database (it will be empty on first run).
            Dictionary<string, Book> booksByName= db.Books
                .AsNoTracking().ToDictionary(b => b.BookTitle, StringComparer.OrdinalIgnoreCase);

            CsvConfiguration config = new(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                HeaderValidated = null
            };

            using StreamReader reader = new(_bookPathName);
            using CsvReader csv = new(reader, config);

            List<BookCSV> records = csv.GetRecords<BookCSV>().ToList();
            foreach (BookCSV record in records)
            {
                if (booksByName.ContainsKey(record.BookTitle))
                {
                    continue;
                }

                Book book = new()
                {
                    BookIsbn13 = record.BookIsbn13,
                    BookIsbn10 = record.BookIsbn10,
                    BookTitle = record.BookTitle,
                    BookAuthor = record.BookAuthor,
                    BookPublisher = record.BookPublisher,
                    BookPublishYear = record.BookPublishYear,
                    BookGenre = record.BookGenre
                };
                await db.Books.AddAsync(book);
                booksByName.Add(record.BookTitle, book);
            }

            await db.SaveChangesAsync();

            return new JsonResult(booksByName.Count);
        }
    }

    }
