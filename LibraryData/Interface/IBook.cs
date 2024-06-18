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
        Task<List<T>> SearchTheBook<T>(string searchBooks) where T : class;
        Task<(HttpStatusCode, bool)> AddBook(LibrarianAddBook book);
        Task<(HttpStatusCode, bool)> IssueABook(string bookname, string username);
        Task<(HttpStatusCode, BookIssue)> ReturnABook(string bookname, string username);
        Task<List<BookIssue>> IssuedBooksUser (string username);
        Task<List<BookIssue>> AllIssuedBooks();
        Task<List<BooksWithFine>> BooksReturnWithFine();
        void SaveChanges();
    }
}
