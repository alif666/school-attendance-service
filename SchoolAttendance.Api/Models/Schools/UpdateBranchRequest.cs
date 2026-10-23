using System.ComponentModel.DataAnnotations;

namespace SchoolAttendance.Api.Models.Schools;

public class UpdateBranchRequest
{
    [Required]
    [StringLength(150, MinimumLength = 2)]
    public string BranchName { get; init; } = string.Empty;

    [StringLength(150, MinimumLength = 2)]
    public string? Address { get; init; } = string.Empty;

}