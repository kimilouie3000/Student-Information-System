using System.ComponentModel.DataAnnotations;

namespace Student_Information_System.Models;

public class Department
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    [Display(Name = "Department Name")]
    public string Name { get; set; } = string.Empty;

    [StringLength(250)]
    public string? Description { get; set; }

    public ICollection<Course> Courses { get; set; } = new List<Course>();
    public ICollection<Instructor> Instructors { get; set; } = new List<Instructor>();
}
