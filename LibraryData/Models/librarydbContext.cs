using System;
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
        => optionsBuilder.UseSqlServer("Data Source=CEI2103\\SQLEXPRESS;Initial Catalog=librarydb;Integrated Security=True;Connect Timeout=60;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BookDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BookDeta__3214EC0791D73274");

            entity.Property(e => e.BookAuthor).HasMaxLength(100);
            entity.Property(e => e.BookPublisher).HasMaxLength(100);
            entity.Property(e => e.Bookname).HasMaxLength(100);
            entity.Property(e => e.Category).HasMaxLength(100);
        });

        modelBuilder.Entity<BookIssue>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BookIssu__3214EC0777868C27");

            entity.ToTable("BookIssue");

            entity.Property(e => e.BookAuthor).HasMaxLength(100);
            entity.Property(e => e.BookPublisher).HasMaxLength(100);
            entity.Property(e => e.Bookname).HasMaxLength(100);
            entity.Property(e => e.Category).HasMaxLength(100);
            entity.Property(e => e.Username).HasMaxLength(20);

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.BookIssues)
                .HasForeignKey(d => d.Username)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_BookIssue_Username");
        });

        modelBuilder.Entity<BookRack>(entity =>
        {
            entity.HasKey(e => e.RowStart).HasName("PK__BookRack__649CABEC82C797E3");

            entity.ToTable("BookRack");

            entity.Property(e => e.RowStart).ValueGeneratedNever();
            entity.Property(e => e.Category).HasMaxLength(100);
        });

        modelBuilder.Entity<EbookRent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__EBookRen__3214EC0709550478");

            entity.ToTable("EBookRent");

            entity.Property(e => e.Bookname).HasMaxLength(100);
            entity.Property(e => e.Category).HasMaxLength(100);
            entity.Property(e => e.IssuedOn).HasPrecision(6);
            entity.Property(e => e.RentAmount).HasColumnType("decimal(5, 0)");
            entity.Property(e => e.Username).HasMaxLength(20);

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.EbookRents)
                .HasForeignKey(d => d.Username)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_Username");
        });

        modelBuilder.Entity<Librarian>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Libraria__3214EC0779F2C044");

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
            entity.HasKey(e => e.Id).HasName("PK__ReturnBo__3214EC0747DBB398");

            entity.ToTable("ReturnBook");

            entity.Property(e => e.Bookname).HasMaxLength(100);
            entity.Property(e => e.FineAmount).HasColumnType("decimal(4, 2)");
        });

        modelBuilder.Entity<SearchHistory>(entity =>
        {
            entity.ToTable("SearchHistory");

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
