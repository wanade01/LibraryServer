﻿// <auto-generated />
using System;
using LibraryServer.LibraryModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LibraryServer.Migrations
{
    [DbContext(typeof(LibraryGoldenContext))]
    [Migration("20240426071340_Identity3")]
    partial class Identity3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LibraryServer.LibraryModel.Book", b =>
                {
                    b.Property<int>("BookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("BookID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookId"));

                    b.Property<string>("BookAuthor")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("BookGenre")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("BookIsbn10")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("BookIsbn13")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<short>("BookPublishYear")
                        .HasColumnType("smallint");

                    b.Property<string>("BookPublisher")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("BookTitle")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("BookId");

                    b.ToTable("Book", (string)null);
                });

            modelBuilder.Entity("LibraryServer.LibraryModel.Patron", b =>
                {
                    b.Property<int>("PatronId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("PatronID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PatronId"));

                    b.Property<string>("PatronAddress")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int?>("PatronCheckedBookId")
                        .HasColumnType("int")
                        .HasColumnName("PatronCheckedBookID");

                    b.Property<DateOnly?>("PatronCheckedDate")
                        .HasColumnType("date");

                    b.Property<DateOnly?>("PatronCheckedDueDate")
                        .HasColumnType("date");

                    b.Property<decimal>("PatronCheckedOverdueAmt")
                        .HasColumnType("decimal(9, 2)");

                    b.Property<string>("PatronFname")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("PatronFName");

                    b.Property<string>("PatronLname")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("PatronLName");

                    b.Property<string>("PatronPassword")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PatronUsername")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("PatronId");

                    b.HasIndex("PatronCheckedBookId");

                    b.ToTable("Patron", (string)null);
                });

            modelBuilder.Entity("LibraryServer.LibraryModel.Patron", b =>
                {
                    b.HasOne("LibraryServer.LibraryModel.Book", "PatronCheckedBook")
                        .WithMany("Patrons")
                        .HasForeignKey("PatronCheckedBookId")
                        .HasConstraintName("FK_Patron_Book");

                    b.Navigation("PatronCheckedBook");
                });

            modelBuilder.Entity("LibraryServer.LibraryModel.Book", b =>
                {
                    b.Navigation("Patrons");
                });
#pragma warning restore 612, 618
        }
    }
}
