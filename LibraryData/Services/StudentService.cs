using LibraryData.Interface;
using LibraryData.Exceptions;
using LibraryData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http;
using Microsoft.Extensions.Logging;

namespace LibraryData.Services
{
    public class StudentService : IStudent
    {
        private readonly librarydbContext _libraryContext;
        private readonly ILogger<StudentService> _logger;
        public StudentService(librarydbContext context, ILogger<StudentService> logger) 
        {
            _libraryContext = context;
            _logger = logger;
        }

        public async Task<(HttpStatusCode, bool)> AddStudent(Student student)
        {
            try
            {
                if (student != null)
                {
                    var existingStudent = await _libraryContext.Students.Where(u => u.Firstname == student.Firstname).ToListAsync();
                    if (existingStudent.Any() || existingStudent != null)
                    {
                        throw new ExistingUserException("User Already Exists");
                    }
                    _libraryContext.Students.Add(student);
                    SaveChanges();
                    return (HttpStatusCode.OK, true);
                }
                else
                {
                    return (HttpStatusCode.BadRequest, false);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception is thrown {ex}");
                return (HttpStatusCode.BadRequest, false);
            }
        }

        public async Task<IActionResult> GetStudent(string username, string password)
        {
            try
            {
                var existingStudent = await _libraryContext.Students.Where(u => u.Username == username && u.Password == password).ToListAsync();
                if (existingStudent.Any() || existingStudent != null)
                {
                    return new OkObjectResult("User Exists");
                }
                else
                {
                    return new BadRequestObjectResult("User Does not Exist");
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "GetStudent: An unexpected error occurred.");
                return new StatusCodeResult(500);
            }
        }

        public void SaveChanges()
        {
            _libraryContext.SaveChanges();
        }
    }
}
