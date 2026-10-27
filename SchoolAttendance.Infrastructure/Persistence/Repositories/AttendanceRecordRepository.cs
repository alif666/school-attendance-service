using Microsoft.EntityFrameworkCore;
using Npgsql;
using SchoolAttendance.Application.Attendances;
using SchoolAttendance.Domain.Attendances;

namespace SchoolAttendance.Infrastructure.Persistence.Repositories;

public class AttendanceRecordRepository : IAttendanceRecordRepository
{
    private readonly AttendanceDbContext _dbContext;
    public AttendanceRecordRepository(AttendanceDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task AddAsync(AttendanceRecord attendanceRecord, CancellationToken cancellationToken = default)
    {
        await _dbContext.AttendanceRecords.AddAsync(attendanceRecord, cancellationToken);
        try
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException exception)
            when (
                exception.InnerException is PostgresException postgresException &&
                postgresException.SqlState == PostgresErrorCodes.UniqueViolation &&
                postgresException.ConstraintName == "IX_AttendanceRecords_EnrollmentId_AttendanceDate_EventType"
            )
        {
            throw new DuplicateAttendanceRecordException(attendanceRecord.EnrollmentId, attendanceRecord.AttendanceDate, attendanceRecord.EventType);
        }
    }

    public async Task<AttendanceRecord?> GetByEnrollmentIdAndAttendanceDateAndEventTypeAsync(Guid enrollmentId, DateOnly attendanceDate, AttendanceEventType eventType, CancellationToken cancellationToken = default)
    {
        return await _dbContext.AttendanceRecords.AsNoTracking().Where(attendanceRecord => attendanceRecord.EnrollmentId == enrollmentId).FirstOrDefaultAsync(attendanceRecord => attendanceRecord.AttendanceDate == attendanceDate && attendanceRecord.EventType == eventType, cancellationToken);
    }

    public async Task<AttendanceRecord?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.AttendanceRecords.AsNoTracking().FirstOrDefaultAsync(attendanceRecord => attendanceRecord.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<AttendanceRecord>> GetBySectionIdAndDateRangeAsync(Guid sectionId, Guid academicSessionId, DateOnly fromDate, DateOnly toDate, CancellationToken cancellationToken = default)
    {
        return await _dbContext.AttendanceRecords.AsNoTracking().Where(attendanceRecord =>
        attendanceRecord.AttendanceDate >= fromDate &&
        attendanceRecord.AttendanceDate <= toDate &&
        _dbContext.Enrollments.Any(enrollment =>
            enrollment.Id == attendanceRecord.EnrollmentId &&
            enrollment.SectionId == sectionId &&
            enrollment.AcademicSessionId == academicSessionId &&
            enrollment.IsActive)).OrderBy(attendanceRecord => attendanceRecord.AttendanceDate).ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(AttendanceRecord attendanceRecord, CancellationToken cancellationToken = default)
    {
        _dbContext.AttendanceRecords.Update(attendanceRecord);
        try
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException exception)
            when (
                exception.InnerException is PostgresException postgresException &&
                postgresException.SqlState == PostgresErrorCodes.UniqueViolation &&
                postgresException.ConstraintName == "IX_AttendanceRecords_EnrollmentId_AttendanceDate_EventType"
            )
        {
            throw new DuplicateAttendanceRecordException(attendanceRecord.EnrollmentId, attendanceRecord.AttendanceDate, attendanceRecord.EventType);
        }
    }
}
