using System;
using System.Collections.Generic;

namespace LibraryData.Models;

public partial class BookRack
{
    public int RowStart { get; set; }

    public int RowEnd { get; set; }

    public string Category { get; set; } = null!;
}
