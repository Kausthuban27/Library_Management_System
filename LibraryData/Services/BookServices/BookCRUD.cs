using LibraryData.Exceptions;
using LibraryData.Interface;
using LibraryData.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LibraryData.Services.BookServices
{
    public class BookCRUD : IBook
    {
        private readonly librarydbContext _library;
        private readonly ILogger<BookCRUD> _logger;
        public BookCRUD(librarydbContext librarydbContext, ILogger<BookCRUD> logger) 
        {
            _library = librarydbContext;
            _logger = logger;
        } 
        public async Task<(HttpStatusCode, bool)> AddBook(BookDetail book)
        {
            try
            {
                var existingBook = await _library.BookDetails.Where(n => n.Bookname == book.Bookname && n.BookAuthor == book.BookAuthor).ToListAsync();
                if (existingBook != null && existingBook.Any())
                {
                    throw new ExistingItemException("Book already Exists");
                }
                else
                {
                    _library.Add(book);
                    SaveChanges();
                    return (HttpStatusCode.OK, true);
                }
            }
            catch(Exception ex) 
            {
                _logger.LogError($"The following Exception occurred {ex}");
                return (HttpStatusCode.InternalServerError, false);
            }
        }
        public async Task<List<BookDetail>> SearchBook(SearchBooks searchBooks)
        {
            try
            {
                var books = _library.BookDetails.AsQueryable();
                books = books.Where(b => (string.IsNullOrEmpty(searchBooks.bookName) || b.Bookname.Equals(searchBooks.bookName, StringComparison.OrdinalIgnoreCase)) && 
                                                        (string.IsNullOrEmpty(searchBooks.authorName) || b.BookAuthor.Equals(searchBooks.authorName, StringComparison.OrdinalIgnoreCase)) &&
                                                        (string.IsNullOrEmpty(searchBooks.publisherName) || b.BookPublisher.Equals(searchBooks.publisherName, StringComparison.OrdinalIgnoreCase)) &&
                                                        (string.IsNullOrEmpty(searchBooks.categoryName) || b.Category.Equals(searchBooks.categoryName, StringComparison.OrdinalIgnoreCase)));
                return await books.ToListAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError($"The following Exception occured {ex}");
                return new List<BookDetail> { };
            }
        }
        public void SaveChanges()
        {
            _library.SaveChangesAsync(); 
        }
    }
}
