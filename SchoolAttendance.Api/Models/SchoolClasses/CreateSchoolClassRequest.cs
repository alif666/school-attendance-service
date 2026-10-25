using System.ComponentModel.DataAnnotations;

namespace SchoolAttendance.Api.Models.SchoolClasses;

public class CreateSchoolClassRequest
{
    [Required]
    public Guid BranchId { get; set; }
    [Required]
    [StringLength(30)]
    public string Name { get; set; } = string.Empty;
}