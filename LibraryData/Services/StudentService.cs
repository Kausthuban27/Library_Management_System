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

namespace LibraryData.Services
{
    public class StudentService: IStudent
    {
        private readonly librarydbContext _libraryContext;
        public StudentService(librarydbContext context) 
        {
            _libraryContext = context;
        }

        public async Task<IActionResult> AddStudent(Student student)
        {
            try
            {
                if (student == null)
                {
                    return new BadRequestObjectResult("All fields are mandatory");
                }
                else
                {
                    var existingStudent = await _libraryContext.Students.Where(u => u.Username == student.Username).ToListAsync();
                    if(existingStudent.Any() || existingStudent != null)
                    {
                        throw new ExistingUserException("User Already Exists");
                    }
                }
                _libraryContext.Students.Add(student);
                SaveChanges();
                return new OkObjectResult("User Added Successfully");
            }
            catch (Exception)
            {
                return new InternalServerErrorResult();
            }
        }

        public Task<(HttpStatusCode, bool)> GetStudent(string username, string password)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            _libraryContext.SaveChanges();
        }
    }
}
