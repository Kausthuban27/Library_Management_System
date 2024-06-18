using Library_WebApp.Model;
using LibraryData.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Library_WebApp.Services
{
    public interface IStudentCRUD
    {
        public Task<(HttpStatusCode, bool)> GetStudent(Uri BaseUrl, string username, string password);
        public Task<(HttpStatusCode, bool)> AddStudent<T>(Uri BaseUrl, T entity) where T : class;
        public Task<(HttpStatusCode, bool)> Rentbook(Uri BaseUrl, string bookname, string username);
        public Task<List<EbookRent>> ebookRents(Uri BaseUrl, string username);
        public Task<(HttpStatusCode, BookIssue)> ReturnBorrowedBook(Uri BaseUrl, string bookname, string username);
        public Task<List<BookDetail>> searchBook(Uri BaseUrl, string? bookname, string? authorname, string? publishername, string? categoryname);
        public Task<List<EbookRentReport>> GetEbookRentReports(Uri BaseUrl);
        public Task<List<BookIssue>> userIssuedBooks(Uri BaseUrl, string username);
        public Task<List<BooksWithFine>> booksWithFine(Uri BaseUrl);
    }
}
