using System;
using System.Collections.Generic;

namespace LibraryData.Models;

public partial class ReturnBook
{
    public int Id { get; set; }

    public string Bookname { get; set; } = null!;

    public bool IsFineApplicable { get; set; }

    public decimal FineAmount { get; set; }
}
