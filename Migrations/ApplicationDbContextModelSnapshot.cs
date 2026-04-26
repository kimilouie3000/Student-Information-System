using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Student_Information_System.Data;

#nullable disable

namespace Student_Information_System.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Student_Information_System.Models.Course", b =>
                {
                    b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");
                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));
                    b.Property<int>("DepartmentId").HasColumnType("int");
                    b.Property<int>("DurationInMonths").HasColumnType("int");
                    b.Property<string>("Name").IsRequired().HasMaxLength(100).HasColumnType("nvarchar(100)");
                    b.HasKey("Id");
                    b.HasIndex("DepartmentId");
                    b.ToTable("Courses");
                });

            modelBuilder.Entity("Student_Information_System.Models.Department", b =>
                {
                    b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");
                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));
                    b.Property<string>("Description").HasMaxLength(250).HasColumnType("nvarchar(250)");
                    b.Property<string>("Name").IsRequired().HasMaxLength(100).HasColumnType("nvarchar(100)");
                    b.HasKey("Id");
                    b.ToTable("Departments");
                });

            modelBuilder.Entity("Student_Information_System.Models.Enrollment", b =>
                {
                    b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");
                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));
                    b.Property<DateTime>("EnrollmentDate").HasColumnType("datetime2");
                    b.Property<decimal?>("Grade").HasColumnType("decimal(18,2)");
                    b.Property<int>("StudentId").HasColumnType("int");
                    b.Property<int>("SubjectId").HasColumnType("int");
                    b.HasKey("Id");
                    b.HasIndex("StudentId", "SubjectId").IsUnique();
                    b.HasIndex("SubjectId");
                    b.ToTable("Enrollments");
                });

            modelBuilder.Entity("Student_Information_System.Models.Instructor", b =>
                {
                    b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");
                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));
                    b.Property<int>("DepartmentId").HasColumnType("int");
                    b.Property<string>("Email").IsRequired().HasMaxLength(150).HasColumnType("nvarchar(150)");
                    b.Property<string>("FullName").IsRequired().HasMaxLength(100).HasColumnType("nvarchar(100)");
                    b.HasKey("Id");
                    b.HasIndex("DepartmentId");
                    b.ToTable("Instructors");
                });

            modelBuilder.Entity("Student_Information_System.Models.Student", b =>
                {
                    b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");
                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));
                    b.Property<int>("CourseId").HasColumnType("int");
                    b.Property<DateTime>("DateOfBirth").HasColumnType("datetime2");
                    b.Property<string>("Email").IsRequired().HasMaxLength(150).HasColumnType("nvarchar(150)");
                    b.Property<string>("FullName").IsRequired().HasMaxLength(100).HasColumnType("nvarchar(100)");
                    b.HasKey("Id");
                    b.HasIndex("CourseId");
                    b.ToTable("Students");
                });

            modelBuilder.Entity("Student_Information_System.Models.Subject", b =>
                {
                    b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");
                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));
                    b.Property<int>("InstructorId").HasColumnType("int");
                    b.Property<string>("Name").IsRequired().HasMaxLength(100).HasColumnType("nvarchar(100)");
                    b.HasKey("Id");
                    b.HasIndex("InstructorId");
                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("Student_Information_System.Models.Course", b =>
                {
                    b.HasOne("Student_Information_System.Models.Department", "Department")
                        .WithMany("Courses")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("Student_Information_System.Models.Enrollment", b =>
                {
                    b.HasOne("Student_Information_System.Models.Student", "Student")
                        .WithMany("Enrollments")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Student_Information_System.Models.Subject", "Subject")
                        .WithMany("Enrollments")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");
                    b.Navigation("Subject");
                });

            modelBuilder.Entity("Student_Information_System.Models.Instructor", b =>
                {
                    b.HasOne("Student_Information_System.Models.Department", "Department")
                        .WithMany("Instructors")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("Student_Information_System.Models.Student", b =>
                {
                    b.HasOne("Student_Information_System.Models.Course", "Course")
                        .WithMany("Students")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("Student_Information_System.Models.Subject", b =>
                {
                    b.HasOne("Student_Information_System.Models.Instructor", "Instructor")
                        .WithMany("Subjects")
                        .HasForeignKey("InstructorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Instructor");
                });
        }
    }
}
