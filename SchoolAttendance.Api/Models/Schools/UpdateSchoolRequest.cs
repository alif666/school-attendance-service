using System.ComponentModel.DataAnnotations;

namespace SchoolAttendance.Api.Models.Schools;

public class UpdateSchoolRequest
{
    [Required]
    [StringLength(150, MinimumLength =2)]
    public string SchoolName {get; init; } = string.Empty;
}