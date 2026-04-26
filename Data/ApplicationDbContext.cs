using Microsoft.EntityFrameworkCore;
using Student_Information_System.Models;

namespace Student_Information_System.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<Instructor> Instructors => Set<Instructor>();
    public DbSet<Student> Students => Set<Student>();
    public DbSet<Subject> Subjects => Set<Subject>();
    public DbSet<Enrollment> Enrollments => Set<Enrollment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Department>()
            .HasMany(d => d.Courses)
            .WithOne(c => c.Department)
            .HasForeignKey(c => c.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Department>()
            .HasMany(d => d.Instructors)
            .WithOne(i => i.Department)
            .HasForeignKey(i => i.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Course>()
            .HasMany(c => c.Students)
            .WithOne(s => s.Course)
            .HasForeignKey(s => s.CourseId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Instructor>()
            .HasMany(i => i.Subjects)
            .WithOne(s => s.Instructor)
            .HasForeignKey(s => s.InstructorId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Enrollment>()
            .HasIndex(e => new { e.StudentId, e.SubjectId })
            .IsUnique();

        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.Student)
            .WithMany(s => s.Enrollments)
            .HasForeignKey(e => e.StudentId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.Subject)
            .WithMany(s => s.Enrollments)
            .HasForeignKey(e => e.SubjectId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
