using System.ComponentModel.DataAnnotations;

namespace SchoolAttendance.Api.Models.Students;

public class CreateStudentRequest
{
    [Required]
    [StringLength(30)]
    public string StudentCode { get; set; } = string.Empty;

    [Required]
    [StringLength(150, MinimumLength = 2)]
    public string FullName { get; set; } = string.Empty;

    public DateOnly? DateOfBirth { get; set; }
}