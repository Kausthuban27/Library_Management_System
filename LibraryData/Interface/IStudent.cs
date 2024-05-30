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
        Task<IActionResult> GetStudent(string username, string password);
        Task<(HttpStatusCode, bool)> AddStudent(AddNewStudent student);
        void SaveChanges();
    }
}
