using System.ComponentModel.DataAnnotations;

namespace Student_Information_System.Models;

public class Instructor
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    [Display(Name = "Instructor Name")]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(150)]
    public string Email { get; set; } = string.Empty;

    [Display(Name = "Department")]
    public int DepartmentId { get; set; }

    public Department? Department { get; set; }
    public ICollection<Subject> Subjects { get; set; } = new List<Subject>();
}
