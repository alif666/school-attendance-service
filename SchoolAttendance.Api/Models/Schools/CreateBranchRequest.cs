using System.ComponentModel.DataAnnotations;

namespace SchoolAttendance.Api.Models.Schools;

public class CreateBranchRequest
{

    [Required]
    public Guid SchoolId { get; set; }

    [Required]
    [StringLength(30)]
    public string BranchCode { get; set; } = string.Empty;

    [Required]
    [StringLength(150, MinimumLength = 2)]
    public string BranchName { get; set; } = string.Empty;

    [StringLength(150, MinimumLength = 2)]
    public string? Address { get; set; } = string.Empty;
}