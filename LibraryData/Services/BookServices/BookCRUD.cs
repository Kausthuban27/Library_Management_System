using LibraryData.Exceptions;
using LibraryData.Interface;
using LibraryData.Models;
using LibraryData.Utilities;
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
        private readonly LibrarydbContext _library;
        private readonly ILogger<BookCRUD> _logger;
        public BookCRUD(LibrarydbContext librarydbContext, ILogger<BookCRUD> logger) 
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
        public async Task<List<BookDetail>> SearchTheBook(SearchBooks searchBooks)
        {
            try
            {
                var books = _library.BookDetails.AsQueryable();
                if(!string.IsNullOrEmpty(searchBooks.bookName))
                {
                    books = books.Where(b => b.Bookname.Equals(searchBooks.bookName));
                }
                if(!string.IsNullOrEmpty(searchBooks.authorName))
                {
                    books = books.Where(b => b.BookAuthor.Equals(searchBooks.authorName));
                }
                if(!string.IsNullOrEmpty(searchBooks.publisherName))
                {
                    books = books.Where(b => b.BookPublisher.Equals(searchBooks.publisherName));
                }
                if(!string.IsNullOrEmpty(searchBooks.categoryName))
                {
                    books = books.Where(b => b.Category.Equals(searchBooks.categoryName));
                }

                Console.WriteLine(books.ToString());
                if (books.Any())
                {
                    _library.SearchHistories.Add(new SearchHistory { BookName = searchBooks.bookName, BookAuthor = searchBooks.authorName, BookPublisher = searchBooks.publisherName, Category = searchBooks.categoryName});
                    SaveChanges();
                    return await books.ToListAsync();
                }
                else
                {
                    return new List<BookDetail> { };
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"The following Exception occured {ex}");
                return new List<BookDetail> { };
            }
        }
        public async Task<List<BookIssue>> IssueABook(string bookname)
        {
            try
            {
                var book = await _library.BookDetails.Where(b => b.Bookname == bookname).FirstOrDefaultAsync();
                if (book != null)
                {
                    var issuedBook = LibrarianMapper.MapBookIssued<BookIssue>(book);
                    _library.BookIssues.Add(issuedBook);
                    SaveChanges();
                    return new List<BookIssue> { issuedBook };
                }
                else
                {
                    return new List<BookIssue> { };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Following exception occcured {ex} ");
                return new List<BookIssue> { };
            }
        }
        public async Task<(HttpStatusCode, bool)> ReturnABook(string bookname)
        {
            try
            {
                var issuedBook = await _library.BookIssues.Where(b => b.Bookname == bookname).FirstOrDefaultAsync();
                decimal fineAmount = 0;
                bool fineApplicable = false;
                if (issuedBook != null)
                {
                    var difference = issuedBook.IssueDate - DateTime.Now;
                    var exceededDayes = difference.Days;
                    if(exceededDayes > 0)
                    {
                        fineAmount = exceededDayes * 100;
                        fineApplicable = true;       
                    }
                    _library.ReturnBooks.Add(new ReturnBook { Bookname = bookname, FineAmount = fineAmount, IsFineApplicable = fineApplicable});
                    SaveChanges();
                    return (HttpStatusCode.OK, true);
                }
                else
                {
                    return (HttpStatusCode.BadRequest, false);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}", ex);
                return(HttpStatusCode.InternalServerError, false);
            }
        }
        public void SaveChanges()
        {
            _library.SaveChangesAsync(); 
        }
    }
}
