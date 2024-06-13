using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryData.Models
{
    public class StudentRentedBook
    {
        public string BookName { get; set; } = null!;
        public string AuthorName { get; set; } = null!;
        public string Publisher { get; set; } = null!;
        public string Category { get; set; } = null!;
        public bool IsRented { get; set; }
        public decimal RentAmount { get; set; }
        public string Username { get; set; } = null!;
    }
}
