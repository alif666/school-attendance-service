using System.ComponentModel.DataAnnotations;

namespace SchoolAttendance.Api.Models.Enrollments;
public class UpdateEnrollmentRequest
{
    [Required]
    public Guid SectionId { get; init; }
}