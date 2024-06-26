﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryServer.LibraryModel;
using Microsoft.AspNetCore.Authorization;

namespace LibraryServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PatronController(LibraryGoldenContext context) : ControllerBase
    {
        // GET: api/Patron
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patron>>> GetPatron()
        {
            return await context.Patrons.ToListAsync();
        }
    }
}
