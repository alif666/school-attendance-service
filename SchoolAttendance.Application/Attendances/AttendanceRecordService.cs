using SchoolAttendance.Application.Enrollments;
using SchoolAttendance.Domain.Attendances;

namespace SchoolAttendance.Application.Attendances;

public class AttendanceRecordService : IAttendanceRecordService
{
    private readonly IAttendanceRecordRepository _attendanceRecordRepository;
    private readonly IEnrollmentRepository _enrollmentRepository;
    public AttendanceRecordService(IAttendanceRecordRepository attendanceRecordRepository, IEnrollmentRepository enrollmentRepository)
    {
        _attendanceRecordRepository = attendanceRecordRepository;
        _enrollmentRepository = enrollmentRepository;
    }

    public async Task<AttendanceRecord> CreateAttendanceRecordAsync(Guid enrollmentId, DateOnly attendanceDate, AttendanceEventType eventType, AttendanceStatus status, CancellationToken cancellationToken = default)
    {
        var attendanceRecord = new AttendanceRecord(
            enrollmentId,
            attendanceDate,
            eventType,
            status
        );
        var enrollment = await _enrollmentRepository.GetByIdAsync(enrollmentId, cancellationToken);
        if (enrollment is null)
        {
            throw new EnrollmentNotFoundException(enrollmentId);
        }
        await _attendanceRecordRepository.AddAsync(attendanceRecord, cancellationToken);
        return attendanceRecord;
    }

    public async Task<AttendanceRecord?> GetByEnrollmentIdAndAttendanceDateAndEventTypeAsync(Guid enrollmentId, DateOnly attendanceDate, AttendanceEventType eventType, CancellationToken cancellationToken = default)
    {
        return await _attendanceRecordRepository.GetByEnrollmentIdAndAttendanceDateAndEventTypeAsync(enrollmentId, attendanceDate, eventType, cancellationToken);
    }

    public async Task<AttendanceRecord?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _attendanceRecordRepository.GetByIdAsync(id, cancellationToken);
    }

    public async Task<IReadOnlyList<AttendanceRecord>> GetBySectionIdAndDateRangeAsync(Guid sectionId, Guid academicSessionId, DateOnly fromDate, DateOnly toDate, CancellationToken cancellationToken = default)
    {
        if (fromDate > toDate)
        {
            throw new ArgumentException("From date cannot be later than to date.", nameof(fromDate));
        }
        return await _attendanceRecordRepository.GetBySectionIdAndDateRangeAsync(sectionId, academicSessionId, fromDate, toDate, cancellationToken);
    }

    public async Task<AttendanceRecord?> UpdateAttendanceRecordAsync(Guid id, AttendanceStatus status, CancellationToken cancellationToken = default)
    {
        var attendanceRecord = await _attendanceRecordRepository.GetByIdAsync(id, cancellationToken);
        if (attendanceRecord is null)
        {
            return null;
        }
        attendanceRecord.ChangeStatus(status);
        await _attendanceRecordRepository.UpdateAsync(attendanceRecord, cancellationToken);
        return attendanceRecord;
    }
}
