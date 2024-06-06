using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryData.Models
{
    public partial class LibrarianAddBook
    {
        public string Bookname { get; set; } = null!;

        public string BookAuthor { get; set; } = null!;

        public string BookPublisher { get; set; } = null!;

        public string Category { get; set; } = null!;
    }
}
