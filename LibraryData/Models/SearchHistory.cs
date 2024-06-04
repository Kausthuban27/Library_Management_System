using System;
using System.Collections.Generic;

namespace LibraryData.Models;

public partial class SearchHistory
{
    public int Id { get; set; }

    public string? BookName { get; set; }

    public string? BookAuthor { get; set; }

    public string? BookPublisher { get; set; }

    public string? Category { get; set; }
}
