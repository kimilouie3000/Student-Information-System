using System.ComponentModel.DataAnnotations;

namespace Student_Information_System.Models;

public class Course
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    [Display(Name = "Course Name")]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Duration (Months)")]
    [Range(1, 72)]
    public int DurationInMonths { get; set; }

    [Display(Name = "Department")]
    public int DepartmentId { get; set; }

    public Department? Department { get; set; }
    public ICollection<Student> Students { get; set; } = new List<Student>();
}
