﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable enable
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LibraryData.Models;

public partial class BookDetail
{
    [JsonIgnore]
    public int Id { get; set; }

    public string Bookname { get; set; } = null!;

    public string BookAuthor { get; set; } = null!;

    public string BookPublisher { get; set; } = null!;

    public string Category { get; set; } = null!;
}