// <auto-generated />
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
    [Migration("20230117075259_mig11")]
    partial class mig11
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EntityLayer.Concrete.Department", b =>
                {
                    b.Property<int>("DepartmentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatedID")
                        .HasColumnType("int");

                    b.Property<string>("DepartmentDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DepartmentName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UpdateID")
                        .HasColumnType("int");

                    b.HasKey("DepartmentID");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("EntityLayer.Concrete.Person", b =>
                {
                    b.Property<int>("PersonID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatedID")
                        .HasColumnType("int");

                    b.Property<int?>("DepartmentID")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("PersonEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PersonFirstName")
                        .HasColumnType("nvarchar(max)");

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

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UpdateID")
                        .HasColumnType("int");

                    b.HasKey("PersonID");

                    b.HasIndex("DepartmentID");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("EntityLayer.Concrete.Person", b =>
                {
                    b.HasOne("EntityLayer.Concrete.Department", "Department")
                        .WithMany("Persons")
                        .HasForeignKey("DepartmentID");

                    b.Navigation("Department");
                });

            modelBuilder.Entity("EntityLayer.Concrete.Department", b =>
                {
                    b.Navigation("Persons");
                });
#pragma warning restore 612, 618
        }
    }
}
