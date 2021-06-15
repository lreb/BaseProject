﻿// <auto-generated />
using System;
using BaseProjectAPI.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace BaseProjectAPI.Persistence.Migrations
{
    [DbContext(typeof(BaseDataContext))]
    [Migration("20210615213557_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("BaseProjectAPI.Domain.Models.Item", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("DisabledOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Items");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            CreatedOn = new DateTimeOffset(new DateTime(2018, 3, 28, 15, 7, 15, 150, DateTimeKind.Unspecified).AddTicks(8943), new TimeSpan(0, -7, 0, 0, 0)),
                            DisabledOn = new DateTimeOffset(new DateTime(2017, 7, 25, 8, 21, 43, 280, DateTimeKind.Unspecified).AddTicks(2598), new TimeSpan(0, -6, 0, 0, 0)),
                            IsEnabled = true,
                            Name = "Mr. Sandra Kutch",
                            Quantity = 3
                        },
                        new
                        {
                            Id = 2L,
                            CreatedOn = new DateTimeOffset(new DateTime(2014, 12, 28, 16, 43, 17, 377, DateTimeKind.Unspecified).AddTicks(879), new TimeSpan(0, -7, 0, 0, 0)),
                            DisabledOn = new DateTimeOffset(new DateTime(2012, 7, 21, 19, 1, 3, 65, DateTimeKind.Unspecified).AddTicks(2321), new TimeSpan(0, -6, 0, 0, 0)),
                            IsEnabled = false,
                            Name = "Mr. Jenny Shanahan",
                            Quantity = 5
                        },
                        new
                        {
                            Id = 3L,
                            CreatedOn = new DateTimeOffset(new DateTime(2012, 8, 2, 22, 36, 14, 810, DateTimeKind.Unspecified).AddTicks(592), new TimeSpan(0, -6, 0, 0, 0)),
                            DisabledOn = new DateTimeOffset(new DateTime(2017, 8, 24, 12, 50, 39, 865, DateTimeKind.Unspecified).AddTicks(312), new TimeSpan(0, -6, 0, 0, 0)),
                            IsEnabled = true,
                            Name = "Miss Beatrice Schultz",
                            Quantity = 4
                        },
                        new
                        {
                            Id = 4L,
                            CreatedOn = new DateTimeOffset(new DateTime(2013, 11, 11, 8, 34, 36, 807, DateTimeKind.Unspecified).AddTicks(8755), new TimeSpan(0, -7, 0, 0, 0)),
                            DisabledOn = new DateTimeOffset(new DateTime(2012, 7, 8, 10, 47, 2, 935, DateTimeKind.Unspecified).AddTicks(2497), new TimeSpan(0, -6, 0, 0, 0)),
                            IsEnabled = false,
                            Name = "Sally Reilly II",
                            Quantity = 3
                        },
                        new
                        {
                            Id = 5L,
                            CreatedOn = new DateTimeOffset(new DateTime(2013, 4, 19, 6, 0, 51, 387, DateTimeKind.Unspecified).AddTicks(6307), new TimeSpan(0, -6, 0, 0, 0)),
                            DisabledOn = new DateTimeOffset(new DateTime(2019, 7, 26, 10, 21, 15, 672, DateTimeKind.Unspecified).AddTicks(7169), new TimeSpan(0, -6, 0, 0, 0)),
                            IsEnabled = true,
                            Name = "Dr. Marilyn Nicolas",
                            Quantity = 10
                        },
                        new
                        {
                            Id = 6L,
                            CreatedOn = new DateTimeOffset(new DateTime(2013, 7, 24, 17, 26, 50, 585, DateTimeKind.Unspecified).AddTicks(5670), new TimeSpan(0, -6, 0, 0, 0)),
                            DisabledOn = new DateTimeOffset(new DateTime(2021, 3, 21, 5, 6, 11, 926, DateTimeKind.Unspecified).AddTicks(12), new TimeSpan(0, -7, 0, 0, 0)),
                            IsEnabled = false,
                            Name = "Mr. Linda Ritchie",
                            Quantity = 3
                        },
                        new
                        {
                            Id = 7L,
                            CreatedOn = new DateTimeOffset(new DateTime(2018, 10, 19, 5, 29, 20, 806, DateTimeKind.Unspecified).AddTicks(9848), new TimeSpan(0, -6, 0, 0, 0)),
                            DisabledOn = new DateTimeOffset(new DateTime(2011, 7, 27, 16, 45, 25, 484, DateTimeKind.Unspecified).AddTicks(4246), new TimeSpan(0, -6, 0, 0, 0)),
                            IsEnabled = false,
                            Name = "Dr. Janice Feeney",
                            Quantity = 2
                        },
                        new
                        {
                            Id = 8L,
                            CreatedOn = new DateTimeOffset(new DateTime(2020, 10, 20, 11, 1, 15, 125, DateTimeKind.Unspecified).AddTicks(6559), new TimeSpan(0, -6, 0, 0, 0)),
                            DisabledOn = new DateTimeOffset(new DateTime(2016, 7, 30, 20, 48, 38, 994, DateTimeKind.Unspecified).AddTicks(6573), new TimeSpan(0, -6, 0, 0, 0)),
                            IsEnabled = false,
                            Name = "Anne Fritsch DDS",
                            Quantity = 5
                        },
                        new
                        {
                            Id = 9L,
                            CreatedOn = new DateTimeOffset(new DateTime(2016, 11, 27, 18, 20, 16, 548, DateTimeKind.Unspecified).AddTicks(6367), new TimeSpan(0, -7, 0, 0, 0)),
                            DisabledOn = new DateTimeOffset(new DateTime(2012, 8, 22, 1, 52, 15, 835, DateTimeKind.Unspecified).AddTicks(8324), new TimeSpan(0, -6, 0, 0, 0)),
                            IsEnabled = true,
                            Name = "Arnold Leannon I",
                            Quantity = 10
                        },
                        new
                        {
                            Id = 10L,
                            CreatedOn = new DateTimeOffset(new DateTime(2012, 11, 29, 0, 57, 50, 574, DateTimeKind.Unspecified).AddTicks(6071), new TimeSpan(0, -7, 0, 0, 0)),
                            DisabledOn = new DateTimeOffset(new DateTime(2015, 3, 8, 4, 39, 35, 146, DateTimeKind.Unspecified).AddTicks(6657), new TimeSpan(0, -7, 0, 0, 0)),
                            IsEnabled = true,
                            Name = "Dr. Freda Cormier",
                            Quantity = 6
                        });
                });
#pragma warning restore 612, 618
        }
    }
}