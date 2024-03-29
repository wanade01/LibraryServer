using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryServer.LibraryModel;

namespace LibraryServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController(LibraryGoldenContext context) : ControllerBase
    {

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            return await context.Books.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book addBook)
        {
            context.Books.Add(addBook);
            await context.SaveChangesAsync();

            return CreatedAtAction(nameof(Book), new {addBook.BookId}, addBook);
        }
    }
}

//{
//    "bookId": 1,
//    "bookIsbn13": "9780141182636",
//    "bookIsbn10": "0141182636",
//    "bookTitle": "NULLThe Great Gatsby",
//    "bookAuthor": "F. Scott Fitzgerald",
//    "bookPublisher": "Scribner",
//    "bookPublishYear": 1925,
//    "bookGenre": "Classic Fiction",
//    "patrons": []
//  }
