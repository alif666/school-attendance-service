using SchoolAttendance.Domain.Attendances;

namespace SchoolAttendance.Application.Attendances;

public sealed class DuplicateAttendanceRecordException : Exception
{
    public DuplicateAttendanceRecordException(Guid enrollmentId, DateOnly attendanceDate, AttendanceEventType eventType) : base($"Enrollment ID '{enrollmentId}' already has '{eventType}' event type on '{attendanceDate}' attendance date.")
    {
    }
}
