using System.ComponentModel.DataAnnotations;

namespace SchoolAttendance.Api.Models.Sections;

public class CreateSectionRequest
{
    [Required]
    public Guid SchoolClassId { get; set; }
    [Required]
    [StringLength(30)]
    public string Name { get; set; } = string.Empty;
}