using System.ComponentModel.DataAnnotations;

namespace Student_Information_System.Models;

public class Subject
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    [Display(Name = "Subject Name")]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Instructor")]
    public int InstructorId { get; set; }

    public Instructor? Instructor { get; set; }
    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}
