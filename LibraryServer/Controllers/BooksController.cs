﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryServer.LibraryModel;
using System.Diagnostics.Metrics;
using Microsoft.AspNetCore.Authorization;

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

        // GET: api/Books/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

    }
}

