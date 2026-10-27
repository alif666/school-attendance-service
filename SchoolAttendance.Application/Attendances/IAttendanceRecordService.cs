using SchoolAttendance.Domain.Attendances;

namespace SchoolAttendance.Application.Attendances;

public interface IAttendanceRecordService
{
    Task<AttendanceRecord> CreateAttendanceRecordAsync(
        Guid enrollmentId,
        DateOnly attendanceDate,
        AttendanceEventType eventType,
        AttendanceStatus status,
        CancellationToken cancellationToken = default
    );
    Task<IReadOnlyList<AttendanceRecord>> GetBySectionIdAndDateRangeAsync(
        Guid sectionId,
        Guid academicSessionId,
        DateOnly fromDate,
        DateOnly toDate,
        CancellationToken cancellationToken = default
    );
    Task<AttendanceRecord?> GetByEnrollmentIdAndAttendanceDateAndEventTypeAsync(
        Guid enrollmentId,
        DateOnly attendanceDate,
        AttendanceEventType eventType,
        CancellationToken cancellationToken = default
    );
    Task<AttendanceRecord?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default
    );
    Task<AttendanceRecord?> UpdateAttendanceRecordAsync(
        Guid id,
        AttendanceStatus status,
        CancellationToken cancellationToken = default
    );
}