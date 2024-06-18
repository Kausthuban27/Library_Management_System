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
using System.Xml;

namespace LibraryData.Interface
{
    public interface IStudent
    {
        Task<IActionResult> GetStudent(string username, string password);
        Task<(HttpStatusCode, bool)> AddStudent(AddNewStudent student);
        Task<(HttpStatusCode, bool)> RentEBook(string bookname, string username);
        Task<List<EbookRent>> RentedBooks(string username);
        Task<List<T>> ExecuteMyStoredProcedureAsync<T>() where T : class;
        void SaveChanges();
    }
}
