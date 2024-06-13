using AutoMapper;
using LibraryData.Exceptions;
using LibraryData.Interface;
using LibraryData.Model;
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
        private readonly LibrarydbContext _libraryContext;
        public LibrarianService(ILogger<LibrarianService> logger, LibrarydbContext librarydbContext)
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

        public async Task<IActionResult> GetLibrarian(string username, string password)
        {
            try
            {
                var librarian = await _libraryContext.Librarians.Where(u => u.Username == username && u.Password == password).ToListAsync();
                if (librarian.Any() && librarian != null)
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

        public async Task<Librarian> RetrieveLibrarianData(string username)
        {
            try
            {
                var librarian = await _libraryContext.Librarians.Where(u => u.Username == username).FirstOrDefaultAsync();
                if(librarian != null)
                {
                    return librarian;
                }
                else
                {
                    throw new Exception("No records Found");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Following exception Occured {ex} ");
                throw new Exception("Program Failed");
            }
        }

        public async Task<IActionResult> UpdateLibrarian(AddNewLibrarian entity)
        {
            if (entity == null)
            {
                return new NotFoundResult();
            }

            _libraryContext.Librarians.Where(u => u.Username == entity.Username)
                    .ExecuteUpdate(b => b.SetProperty(u => u.Firstname, entity.Firstname)
                        .SetProperty(u => u.Lastname, entity.Lastname)
                        .SetProperty(u => u.Password, entity.Password)
                        .SetProperty(u => u.Email, entity.Email)
                        .SetProperty(u => u.Phone, entity.Phone));

                await _libraryContext.SaveChangesAsync();

            return new OkObjectResult("");
        }
    }
}
