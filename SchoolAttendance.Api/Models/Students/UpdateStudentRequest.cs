using System.ComponentModel.DataAnnotations;

namespace SchoolAttendance.Api.Models.Students;

public class UpdateStudentRequest
{
    [Required]
    [StringLength(150, MinimumLength = 2)]
    public string FullName { get; init; } = string.Empty;

    public DateOnly? DateOfBirth { get; init; }
}