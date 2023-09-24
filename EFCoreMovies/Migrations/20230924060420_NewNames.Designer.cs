﻿// <auto-generated />
using EFCoreMovies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EFCoreMovies.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230924060420_NewNames")]
    partial class NewNames
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.11");

            modelBuilder.Entity("EFCoreMovies.Entities.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("TEXT")
                        .HasColumnName("GenreName");

                    b.HasKey("Id");

                    b.ToTable("GenresTbl", "movies");
                });
#pragma warning restore 612, 618
        }
    }
}