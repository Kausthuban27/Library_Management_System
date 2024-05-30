using LibraryData.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LibraryData.Interface
{
    public interface ILibrarian
    {
        Task<IActionResult> GetLibrarian(string username, string password);
        Task<(HttpStatusCode, bool)> AddLibrarian(AddNewLibrarian librarian);
    }
}
