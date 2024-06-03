using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using LibraryData.Models;

namespace LibraryData.Interface
{
    public interface IBook
    {
        Task<List<BookDetail>> SearchBook(SearchBooks searchBooks);
        Task<(HttpStatusCode, bool)> AddBook(BookDetail book);
        void SaveChanges();
    }
}
