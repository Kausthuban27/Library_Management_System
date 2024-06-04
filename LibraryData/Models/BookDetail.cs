using System;
using System.Collections.Generic;

namespace LibraryData.Models;

public partial class BookDetail
{
    public int Id { get; set; }

    public string Bookname { get; set; } = null!;

    public string BookAuthor { get; set; } = null!;

    public string BookPublisher { get; set; } = null!;

    public string Category { get; set; } = null!;
}
