﻿// <auto-generated />
using System;
using Infoteks.DAL.Context.EFContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InfoteksTest.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20221226141828__newInit")]
    partial class _newInit
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Infoteks.Domain.Entities.Results", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<TimeSpan>("AllTime")
                        .HasColumnType("time");

                    b.Property<double>("AverageCompletionTime")
                        .HasColumnType("float");

                    b.Property<double>("AverageIndicator")
                        .HasColumnType("float");

                    b.Property<int>("CountString")
                        .HasColumnType("int");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FirstOperation")
                        .HasColumnType("datetime2");

                    b.Property<double>("MaxIndicator")
                        .HasColumnType("float");

                    b.Property<double>("MedianIndicators")
                        .HasColumnType("float");

                    b.Property<double>("MinIndicator")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Results");
                });

            modelBuilder.Entity("Infoteks.Domain.Entities.Values", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CompletionTime")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("Indicator")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("FileName");

                    b.ToTable("Values");
                });
#pragma warning restore 612, 618
        }
    }
}