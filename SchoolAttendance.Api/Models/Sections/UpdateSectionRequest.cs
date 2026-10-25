using System.ComponentModel.DataAnnotations;

namespace SchoolAttendance.Api.Models.Sections;

public class UpdateSectionRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;
}