﻿// <auto-generated />
using GenerationServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GenerationServer.Migrations
{
    [DbContext(typeof(DBContext))]
    [Migration("20240507034124_sqlite_migration_406")]
    partial class sqlite_migration_406
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.17");

            modelBuilder.Entity("GenerationServer.Models.DiscountCodes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("DiscountCode")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("DiscountCodes");
                });
#pragma warning restore 612, 618
        }
    }
}
