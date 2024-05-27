using LibraryData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LibraryData.Interface
{
    public interface IStudent
    {
        public Task<(HttpStatusCode, bool)> GetStudent(string username, string password);
        public Task<IActionResult> AddStudent(Student student);
        public void SaveChanges();
    }
}
