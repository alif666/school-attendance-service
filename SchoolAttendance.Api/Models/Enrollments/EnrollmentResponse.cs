namespace SchoolAttendance.Api.Models.Enrollments;

public record EnrollmentResponse(
    Guid Id,
    Guid StudentId,
    Guid AcademicSessionId,
    Guid SectionId,
    DateOnly EnrollmentDate
);