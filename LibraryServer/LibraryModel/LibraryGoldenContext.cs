using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibraryServer.LibraryModel;

public partial class LibraryGoldenContext : IdentityDbContext<LibraryUser>
{
    public LibraryGoldenContext()
    {
    }

    public LibraryGoldenContext(DbContextOptions<LibraryGoldenContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Patron> Patrons { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=Waleed-Desktop\\SQLEXPRESS;Initial Catalog=LibraryGolden;Integrated Security=True;encrypt=false;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Book>(entity =>
        {
            entity.ToTable("Book");

            entity.Property(e => e.BookId).HasColumnName("BookID");
            entity.Property(e => e.BookAuthor).HasMaxLength(64);
            entity.Property(e => e.BookGenre).HasMaxLength(64);
            entity.Property(e => e.BookIsbn10).HasMaxLength(10);
            entity.Property(e => e.BookIsbn13).HasMaxLength(13);
            entity.Property(e => e.BookPublisher).HasMaxLength(64);
            entity.Property(e => e.BookTitle).HasMaxLength(150);
        });

        modelBuilder.Entity<Patron>(entity =>
        {
            entity.ToTable("Patron");

            entity.Property(e => e.PatronId).HasColumnName("PatronID");
            entity.Property(e => e.PatronAddress).HasMaxLength(250);
            entity.Property(e => e.PatronCheckedBookId).HasColumnName("PatronCheckedBookID");
            entity.Property(e => e.PatronCheckedOverdueAmt).HasColumnType("decimal(9, 2)");
            entity.Property(e => e.PatronFname)
                .HasMaxLength(30)
                .HasColumnName("PatronFName");
            entity.Property(e => e.PatronLname)
                .HasMaxLength(30)
                .HasColumnName("PatronLName");

            entity.HasOne(d => d.PatronCheckedBook).WithMany(p => p.Patrons)
                .HasForeignKey(d => d.PatronCheckedBookId)
                .HasConstraintName("FK_Patron_Book");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
