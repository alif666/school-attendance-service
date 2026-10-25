namespace SchoolAttendance.Api.Models.AcademicSessions;
public record AcademicSessionResponse(
    Guid Id,
    Guid BranchId,
    string Name,
    DateOnly StartDate,
    DateOnly EndDate
);