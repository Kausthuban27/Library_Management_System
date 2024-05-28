using LibraryData.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LibraryData.Interface
{
    public interface ILibrarian
    {
        Task<IActionResult> GetLibrarian(string username);
        Task<(HttpStatusCode, bool)> AddLibrarian(Librarian librarian);
    }
}
