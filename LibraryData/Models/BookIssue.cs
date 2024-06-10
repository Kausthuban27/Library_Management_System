using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LibraryData.Models;

public partial class BookIssue
{
    public int Id { get; set; }

    public string Bookname { get; set; } = null!;

    public string BookAuthor { get; set; } = null!;

    public string BookPublisher { get; set; } = null!;

    public string Category { get; set; } = null!;

    public DateTime IssueDate { get; set; } = DateTime.Now;

    public string Username { get; set; } = null!;

    [JsonIgnore]
    public virtual Student UsernameNavigation { get; set; } = null!;
}
