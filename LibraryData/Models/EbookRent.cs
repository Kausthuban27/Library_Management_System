using System;
using System.Collections.Generic;

namespace LibraryData.Models;

public partial class EbookRent
{
    public int Id { get; set; }

    public string Bookname { get; set; } = null!;

    public bool IsRented { get; set; }

    public string Category { get; set; } = null!;

    public decimal RentAmount { get; set; }

    public DateTime IssuedOn { get; set; }

    public string Username { get; set; } = null!;

    public virtual Student UsernameNavigation { get; set; } = null!;
}
