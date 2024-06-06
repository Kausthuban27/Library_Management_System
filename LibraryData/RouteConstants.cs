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
    }
}
