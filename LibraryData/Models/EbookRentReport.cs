using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryData.Models
{
    public class EbookRentReport
    {
        public string? Bookname { get; set; }
        public bool IsRented { get; set; }
        public decimal RentAmount { get; set; }
        public string? Username { get; set; }
    }
}
