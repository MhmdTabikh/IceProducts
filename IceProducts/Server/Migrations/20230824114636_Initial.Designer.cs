﻿// <auto-generated />
using System;
using IceProducts.Server.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace IceProducts.Server.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20230824114636_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("IceProducts.Server.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<byte[]>("Image")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<string>("LongDescription")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Sizes")
                        .HasColumnType("text");

                    b.Property<string>("SmallDescription")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("character varying(75)");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("IceProducts.Server.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("dd32ee93-f8f8-406e-bc20-79d7f23ba227"),
                            Email = "ta.a981111@gmail.ccom",
                            Password = "AC2Kuhruh4TlArR0YjIDtMVPX4SRqAjRr2aaPIiuw1omZ3KPeJWw7a7Gzb9lTT2OAg=="
                        });
                });
#pragma warning restore 612, 618
        }
    }
}