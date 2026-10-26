using Microsoft.EntityFrameworkCore;
using Npgsql;
using SchoolAttendance.Application.Enrollments;
using SchoolAttendance.Domain.Enrollments;

namespace SchoolAttendance.Infrastructure.Persistence.Repositories;

public class EnrollmentRepository : IEnrollmentRepository
{
    private readonly AttendanceDbContext _dbContext;
    public EnrollmentRepository(AttendanceDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task AddAsync(Enrollment enrollment, CancellationToken cancellationToken = default)
    {
        await _dbContext.Enrollments.AddAsync(enrollment, cancellationToken);
        try
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException exception)
            when (
                exception.InnerException is PostgresException postgresException &&
                postgresException.SqlState == PostgresErrorCodes.UniqueViolation &&
                postgresException.ConstraintName == "IX_Enrollments_StudentId_AcademicSessionId"
            )
        {
            throw new DuplicateEnrollmentException(enrollment.StudentId, enrollment.AcademicSessionId);
        }
    }

    public async Task<Enrollment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Enrollments.AsNoTracking().Where(enrollment => enrollment.IsActive).FirstOrDefaultAsync(enrollment => enrollment.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Enrollment>> GetBySectionIdAndAcademicSessionIdAsync(Guid sectionId, Guid academicSessionId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Enrollments.AsNoTracking().Where(enrollment => enrollment.IsActive && enrollment.SectionId == sectionId && enrollment.AcademicSessionId == academicSessionId).OrderBy(enrollment => enrollment.EnrollmentDate).ToListAsync(cancellationToken);
    }

    public async Task<Enrollment?> GetByStudentIdAndAcademicSessionIdAsync(Guid studentId, Guid academicSessionId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Enrollments.AsNoTracking().Where(enrollment => enrollment.IsActive).FirstOrDefaultAsync(enrollment => enrollment.StudentId == studentId && enrollment.AcademicSessionId == academicSessionId, cancellationToken);
    }

    public async Task UpdateAsync(Enrollment enrollment, CancellationToken cancellationToken = default)
    {
        _dbContext.Enrollments.Update(enrollment);
        try
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException exception)
            when (
                exception.InnerException is PostgresException postgresException &&
                postgresException.SqlState == PostgresErrorCodes.UniqueViolation &&
                postgresException.ConstraintName == "IX_Enrollments_StudentId_AcademicSessionId"
            )
        {
            throw new DuplicateEnrollmentException(enrollment.StudentId, enrollment.AcademicSessionId);
        }
    }
}