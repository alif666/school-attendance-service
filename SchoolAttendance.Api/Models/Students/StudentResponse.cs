namespace SchoolAttendance.Api.Models.Students;

public record StudentResponse(
    Guid Id,
    string StudentCode,
    string FullName,
    DateOnly? DateOfBirth
);