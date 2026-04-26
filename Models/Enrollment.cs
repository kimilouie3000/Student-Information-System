using System.ComponentModel.DataAnnotations;

namespace Student_Information_System.Models;

public class Enrollment
{
    public int Id { get; set; }

    [Display(Name = "Student")]
    public int StudentId { get; set; }

    [Display(Name = "Subject")]
    public int SubjectId { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Enrollment Date")]
    public DateTime EnrollmentDate { get; set; }

    [Range(0, 100)]
    public decimal? Grade { get; set; }

    public Student? Student { get; set; }
    public Subject? Subject { get; set; }
}
