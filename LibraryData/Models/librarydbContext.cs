﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LibraryData.Models;

public partial class LibrarydbContext : DbContext
{
    public LibrarydbContext()
    {
    }

    public LibrarydbContext(DbContextOptions<LibrarydbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BookDetail> BookDetails { get; set; }

    public virtual DbSet<BookIssue> BookIssues { get; set; }

    public virtual DbSet<BookRack> BookRacks { get; set; }

    public virtual DbSet<EbookRent> EbookRents { get; set; }

    public virtual DbSet<Librarian> Librarians { get; set; }

    public virtual DbSet<ReturnBook> ReturnBooks { get; set; }

    public virtual DbSet<SearchHistory> SearchHistories { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BookDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BookDeta__3214EC075F18547A");

            entity.Property(e => e.BookAuthor).HasMaxLength(100);
            entity.Property(e => e.BookPublisher).HasMaxLength(100);
            entity.Property(e => e.Bookname).HasMaxLength(100);
            entity.Property(e => e.Category).HasMaxLength(100);
        });

        modelBuilder.Entity<BookIssue>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BookIssu__3214EC072B30021B");

            entity.ToTable("BookIssue");

            entity.Property(e => e.BookAuthor).HasMaxLength(100);
            entity.Property(e => e.BookPublisher).HasMaxLength(100);
            entity.Property(e => e.Bookname).HasMaxLength(100);
            entity.Property(e => e.Category).HasMaxLength(100);
        });

        modelBuilder.Entity<BookRack>(entity =>
        {
            entity.HasKey(e => e.RowStart).HasName("PK__BookRack__649CABECAD28F831");

            entity.ToTable("BookRack");

            entity.Property(e => e.RowStart).ValueGeneratedNever();
            entity.Property(e => e.Category).HasMaxLength(100);
        });

        modelBuilder.Entity<EbookRent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC07971D691A");

            entity.ToTable("EBookRent");

            entity.Property(e => e.Bookname)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Category).HasMaxLength(100);
            entity.Property(e => e.RentAmount).HasColumnType("decimal(5, 0)");
        });

        modelBuilder.Entity<Librarian>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC076D459D76");

            entity.ToTable("Librarian");

            entity.Property(e => e.DateOfJoining).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Firstname).HasMaxLength(50);
            entity.Property(e => e.Lastname).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(10);
            entity.Property(e => e.Username).HasMaxLength(20);
        });

        modelBuilder.Entity<ReturnBook>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ReturnBo__3214EC0738EBBC5D");

            entity.ToTable("ReturnBook");

            entity.Property(e => e.Bookname).HasMaxLength(100);
            entity.Property(e => e.FineAmount).HasColumnType("decimal(4, 2)");
        });

        modelBuilder.Entity<SearchHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SearchHi__3214EC0761090966");

            entity.ToTable("SearchHistory");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.BookAuthor).HasMaxLength(100);
            entity.Property(e => e.BookName).HasMaxLength(100);
            entity.Property(e => e.BookPublisher).HasMaxLength(100);
            entity.Property(e => e.Category).HasMaxLength(100);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Username);

            entity.ToTable("Student");

            entity.Property(e => e.Username).HasMaxLength(20);
            entity.Property(e => e.Department).HasMaxLength(10);
            entity.Property(e => e.Firstname).HasMaxLength(50);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Lastname).HasMaxLength(10);
            entity.Property(e => e.Password).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
