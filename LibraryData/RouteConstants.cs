using Microsoft.AspNetCore.Routing;

namespace LibraryData
{
    public class RouteConstants
    {
        public const string Registerstudent = "AddStudent";
        public const string Loginstudent = "GetStudent";
        public const string RegisterLibrarian = "AddLibrarian";
        public const string LoginLibrarian = "GetLibrarian";
        public const string RentBook = "EBookRent";
        public const string SearchBook = "SearchBook";
        public const string AddBook = "AddBook";
        public const string RentedBooks = "ShowRentedBooks";
        public const string IssueBook = "IssueBook";
        public const string GetIssuedBooks = "GetIssuedBooks";
        public const string AllIssuedBooks = "AllIssuedBooks";
        public const string ReturnBook = "ReturnBook";
        public const string BooksWithFineAmount = "BooksWithFineAmount";
        public const string RetrieveLibrarian = "RetrieveLibrarian";
        public const string UpdatedLibrarian = "UpdateLibrarian";
        public const string GetAuthorBasedRentedBooks = "GetAuthorBasedRentedBooks";
        public const string GetMonthlyReport = "GetMonthlyReport";
    }
}
