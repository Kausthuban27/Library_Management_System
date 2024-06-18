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
using LibraryData.Utilities;
using Microsoft.Data.SqlClient;

namespace LibraryData.Services
{
    public class StudentService : IStudent
    {
        private readonly LibrarydbContext _libraryContext;
        private readonly ILogger<StudentService> _logger;
        public StudentService(LibrarydbContext context, ILogger<StudentService> logger) 
        {
            _libraryContext = context;
            _logger = logger;
        }

        public async Task<(HttpStatusCode, bool)> AddStudent(AddNewStudent student)
        {
            try
            {
                if (student != null)
                {
                    var existingStudent = await _libraryContext.Students.Where(u => u.Username == student.Username).ToListAsync();
                    if (existingStudent.Any() && existingStudent!=null)
                    {
                        throw new ExistingUserException("User Already Exists");
                    }
                    var newStudent = LibrarianMapper.MapStudent<Student>(student);
                    _libraryContext.Students.Add(newStudent);
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

        public async Task<List<T>> ExecuteMyStoredProcedureAsync<T>() where T : class
        {
            var parameterValue = new SqlParameter("@p0", DateTime.Now.Date);
            var query = "EXEC [dbo].[Get_Rented_Books_By_Month] @p0";

            var result = await _libraryContext.Set<T>().FromSqlRaw(query, parameterValue).ToListAsync();
            return result;
        }

        public async Task<IActionResult> GetStudent(string username, string password)
        {
            try
            {
                var existingStudent = await _libraryContext.Students.Where(u => u.Username == username && u.Password == password).ToListAsync();
                if (existingStudent.Any())
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

        public async Task<(HttpStatusCode, bool)> RentEBook(string bookname, string username)
        {
            try
            {
                decimal RentAmount = 500;
                var bookAvailable = await _libraryContext.BookDetails.Where(b => b.Bookname == bookname).FirstOrDefaultAsync();
                if (bookAvailable != null)
                {
                    _libraryContext.EbookRents.Add(new EbookRent { Bookname = bookname, IsRented = true, Category = bookAvailable.Category, RentAmount = RentAmount, Username = username });
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
                _logger.LogError($"Exception occured {ex}");
                return (HttpStatusCode.InternalServerError, false);
            }
        }

        public async Task<List<EbookRent>> RentedBooks<T>(string username) where T : class
        {
            try
            {
                var validUser = await _libraryContext.Students.Where(b => b.Username == username).ToListAsync();
                if(validUser.Any() && validUser != null)
                {
                    var rentedBooks = await _libraryContext.EbookRents.Where(b => b.Username == username && b.IsRented).ToListAsync();
                    return rentedBooks;
                }
                else
                {
                    throw new Exception("User Is Invalid");
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"An Exception Occurred : {ex} ");
                return new List<EbookRent> { };
            }
        }

        public void SaveChanges()
        {
            _libraryContext.SaveChanges();
        }
    }
}
