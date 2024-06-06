using System;
using System.Collections.Generic;

namespace LibraryData.Models;

public partial class Student
{
    public int Id { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Department { get; set; } = null!;

    public int Year { get; set; }

    public virtual ICollection<EbookRent> EbookRents { get; set; } = new List<EbookRent>();
}
