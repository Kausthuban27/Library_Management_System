using AutoMapper;
using LibraryData.Exceptions;
using LibraryData.Interface;
using LibraryData.Models;
using LibraryData.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LibraryData.Services
{
    public class LibrarianService : ILibrarian
    {
        private readonly ILogger<LibrarianService> _logger;
        private readonly librarydbContext _libraryContext;
        public LibrarianService(ILogger<LibrarianService> logger, librarydbContext librarydbContext)
        {
            _libraryContext = librarydbContext;
            _logger = logger;
        }
        public async Task<(HttpStatusCode, bool)> AddLibrarian(AddNewLibrarian librarian)
        {
            try
            {
                if (librarian != null)
                {
                    var existingLibrarian = await _libraryContext.Librarians.Where(u => u.Username == librarian.Username).ToListAsync();
                    if (existingLibrarian.Any())
                    {
                        throw new ExistingUserException("User Already Exists");
                    }
                    var librarianMap = LibrarianMapper.MapLibrarian<Librarian>(librarian);
                    Console.WriteLine($"Librarian Map {librarianMap}");
                    _libraryContext.Librarians.Add(librarianMap);
                    _libraryContext.SaveChanges();
                    return (HttpStatusCode.OK, true);
                }
                else
                {
                    return (HttpStatusCode.BadRequest, false);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Add Librarian : Unexpected Error occurred {ex}");
                return (HttpStatusCode.InternalServerError, false); 
            }
        }

        public async Task<IActionResult> GetLibrarian(string username)
        {
            try
            {
                var librarian = await _libraryContext.Librarians.Where(u => u.Username == username).ToListAsync();
                if (librarian != null && librarian.Any())
                {
                    return new OkObjectResult("Librarian Exists");
                }
                else
                {
                    return new BadRequestObjectResult("Librarian Does not Exist");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Get Librarian: An unexpected error occurred.");
                return new StatusCodeResult(500);
            }
        }
    }
}
