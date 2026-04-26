using System.ComponentModel.DataAnnotations;

namespace Student_Information_System.Models;

public class Student
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    [Display(Name = "Student Name")]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(150)]
    public string Email { get; set; } = string.Empty;

    [DataType(DataType.Date)]
    [Display(Name = "Date of Birth")]
    public DateTime DateOfBirth { get; set; }

    [Display(Name = "Course")]
    public int CourseId { get; set; }

    public Course? Course { get; set; }
    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}
