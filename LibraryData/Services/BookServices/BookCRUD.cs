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
        public async Task<(HttpStatusCode, bool)> AddBook(LibrarianAddBook book)
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
                    var newBook = LibrarianMapper.MapNewBook<BookDetail>(book);
                    _library.BookDetails.Add(newBook);
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
        public async Task<(HttpStatusCode, bool)> IssueABook(string bookname, string username)
        {
            try
            {
                var book = await _library.BookDetails.Where(b => b.Bookname == bookname).FirstOrDefaultAsync();
                if (book != null)
                {
                    var issuedBook = LibrarianMapper.MapBookIssued<BookIssue>(book);
                    issuedBook.Username = username;
                    _library.BookIssues.Add(issuedBook);
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
                _logger.LogError($"Following exception occcured {ex} ");
                return (HttpStatusCode.InternalServerError, false);
            }
        }
        public async Task<(HttpStatusCode, BookIssue)> ReturnABook(string bookname, string username)
        {
            try
            {
                var issuedBook = await _library.BookIssues.Where(b => b.Bookname == bookname && b.Username == username).FirstOrDefaultAsync();
                var issueBook = await _library.BookIssues.FirstOrDefaultAsync(b => b.Bookname == bookname && b.Username == username);
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
                    var returnBook = new ReturnBook { Bookname = bookname, FineAmount = fineAmount, IsFineApplicable = fineApplicable };
                    _library.ReturnBooks.Add(returnBook);
                    SaveChanges();
                    return (HttpStatusCode.OK, issueBook!);
                }
                else
                {
                    return (HttpStatusCode.BadRequest, new BookIssue { });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}", ex);
                return(HttpStatusCode.InternalServerError, new BookIssue { });
            }
        }
        public void SaveChanges()
        {
            _library.SaveChanges(); 
        }

        public async Task<List<BookIssue>> IssuedBooksUser(string username)
        {
            try
            {
                var booksList = await _library.BookIssues.Where(u => u.Username == username).ToListAsync();
                if (booksList.Any() && booksList != null)
                {
                    return booksList;
                }
                return new List<BookIssue> { };
            }
            catch (Exception ex)
            {
                _logger.LogError($"The following Exception Occured {ex}");
                return new List<BookIssue> { };
            }
        }

        public async Task<List<BookIssue>> AllIssuedBooks()
        {
            try
            {
                return await _library.BookIssues.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"The following Exception Occured {ex}");
                return new List<BookIssue> { };
            }
        }
    }
}
