namespace SchoolAttendance.Api.Models.SchoolClasses;

public record SchoolClassResponse(
    Guid Id,
    Guid BranchId,
    string Name
);