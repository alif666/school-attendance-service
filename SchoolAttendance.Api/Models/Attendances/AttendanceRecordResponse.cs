using SchoolAttendance.Domain.Attendances;

namespace SchoolAttendance.Api.Models.Attendances;

public record AttendanceRecordResponse(
    Guid Id,
    Guid EnrollmentId,
    DateOnly AttendanceDate,
    AttendanceEventType EventType,
    AttendanceStatus Status
);