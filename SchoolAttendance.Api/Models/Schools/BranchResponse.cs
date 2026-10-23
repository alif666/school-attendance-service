namespace SchoolAttendance.Api.Models.Schools;

public record BranchResponse (
    Guid Id,
    Guid SchoolId,
    string BranchCode,
    string BranchName,
    string? Address
);