namespace SchoolAttendance.Api.Models.Schools;

public record SchoolResponse(
    Guid Id,
    string SchoolCode,
    string SchoolName
);