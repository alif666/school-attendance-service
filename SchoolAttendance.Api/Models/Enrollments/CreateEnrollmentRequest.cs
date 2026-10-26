using System.ComponentModel.DataAnnotations;

namespace SchoolAttendance.Api.Models.Enrollments;

public class CreateEnrollmentRequest
{
    [Required]
    public Guid StudentId { get; set; }
    [Required]
    public Guid AcademicSessionId { get; set; }
    [Required]
    public Guid SectionId { get; set; }
    [Required]
    public DateOnly EnrollmentDate { get; set; }
}