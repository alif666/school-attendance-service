using System.ComponentModel.DataAnnotations;

namespace SchoolAttendance.Api.Models.Schools;

public class CreateSchoolRequest
{
    [Required]
    [StringLength(30)]
    public string SchoolCode { get; set; } = string.Empty;
    [Required]
    [StringLength(150, MinimumLength =2)]
    public string SchoolName { get; set; } = string.Empty;
}