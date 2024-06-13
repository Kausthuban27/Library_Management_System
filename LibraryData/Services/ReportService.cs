using LibraryData.Interface;
using LibraryData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryData.Services
{
    public class ReportService
    {
        readonly IScopedDbContext<LibrarydbContext> scopedDbContext;

        public ReportService()
        {
            throw new NotSupportedException();
        }

        public ReportService(IScopedDbContext<LibrarydbContext> scopedDbContext, IStudent student)
        {
            this.scopedDbContext = scopedDbContext ?? throw new ArgumentNullException(nameof(scopedDbContext));
        }

        public async Task<List<StudentRentedBook>> Get_Student_Rented_Books(string authorname)
        {
            dynamic booksData;
            using (var ScopedLibraryContext = scopedDbContext.GetDbContextScope())
            {
                var dbContext = ScopedLibraryContext.DbContext;
#pragma warning disable CS8601 // Possible null reference assignment.
                var rentedBooks = await dbContext.BookDetails
                    .Where(b => b.BookAuthor == authorname)
                    .Select(b => new StudentRentedBook
                    {
                        BookName = b.Bookname,
                        AuthorName = b.BookAuthor,
                        Publisher = b.BookPublisher,
                        Category = b.Category,
                        IsRented = dbContext.EbookRents
                                          .Where(e => e.Bookname == b.Bookname && e.IsRented)
                                          .Select(e => e.IsRented)
                                          .FirstOrDefault(),
                        RentAmount = dbContext.EbookRents
                                               .Where(e => e.Bookname == b.Bookname && e.RentAmount > 0)
                                               .Select(e => e.RentAmount)
                                               .FirstOrDefault(),
                        Username = dbContext.EbookRents
                                           .Where(e => e.Bookname == b.Bookname)
                                           .Select(e => e.Username)
                                           .FirstOrDefault()
                    }).ToListAsync();
                booksData = rentedBooks;
#pragma warning restore CS8601 // Possible null reference assignment.
            }
            return booksData;
        }
    }
}
