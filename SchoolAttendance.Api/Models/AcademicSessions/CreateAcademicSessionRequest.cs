using System.ComponentModel.DataAnnotations;

namespace SchoolAttendance.Api.Models.AcademicSessions;

public class CreateAcademicSessionRequest
{
    [Required]
    public Guid BranchId { get; set; }

    [Required]
    [StringLength(30)]
    public string Name { get; set; } = string.Empty;

    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
}