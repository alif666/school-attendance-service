using System.ComponentModel.DataAnnotations;

namespace SchoolAttendance.Api.Models.AcademicSessions;

public class UpdateAcademicSessionRequest
{

    [Required]
    [StringLength(30)]
    public string Name { get; init; } = string.Empty;

    public DateOnly StartDate { get; init; }
    public DateOnly EndDate { get; init; }
}