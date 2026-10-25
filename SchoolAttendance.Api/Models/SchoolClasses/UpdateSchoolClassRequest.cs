using System.ComponentModel.DataAnnotations;

namespace SchoolAttendance.Api.Models.SchoolClasses;

public class UpdateSchoolClassRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;
}