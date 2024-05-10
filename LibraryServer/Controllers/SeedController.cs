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
namespace LibraryServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedController(LibraryGoldenContext db, IHostEnvironment environment,
        UserManager<LibraryUser> userManager) : ControllerBase
    {
        private readonly string _pathName = Path.Combine(environment.ContentRootPath, "Data/patrons.csv");
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

        //[HttpPost("Book")]
        //public async Task<ActionResult<Book>> SeedBook()
        //{

        //}

    }
}
