﻿// <auto-generated />
using System;
using DataAccessLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataAccessLayer.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20230111122218_mig3")]
    partial class mig3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EntityLayer.Concrete.Person", b =>
                {
                    b.Property<int>("PersonID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("PersonCreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PersonCreatedID")
                        .HasColumnType("int");

                    b.Property<string>("PersonEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PersonFirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PersonIsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("PersonIsAdmin")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("PersonLastActivited")
                        .HasColumnType("datetime2");

                    b.Property<string>("PersonLastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PersonPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PersonPhone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PersonToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("PersonUpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("PersonUpdateID")
                        .HasColumnType("int");

                    b.HasKey("PersonID");

                    b.ToTable("Persons");
                });
#pragma warning restore 612, 618
        }
    }
}
