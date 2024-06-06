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
        Task<List<BookDetail>> SearchTheBook(SearchBooks searchBooks);
        Task<(HttpStatusCode, bool)> AddBook(LibrarianAddBook book);
        Task<List<BookIssue>> IssueABook(string bookname);
        Task<(HttpStatusCode, bool)> ReturnABook(string bookname);
        void SaveChanges();
    }
}
