using SchoolAttendance.Domain.Attendances;

namespace SchoolAttendance.Application.Attendances;

public interface IAttendanceRecordRepository
{
    Task AddAsync(
        AttendanceRecord attendanceRecord,
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
    Task UpdateAsync(AttendanceRecord attendanceRecord, CancellationToken cancellationToken = default);
}
