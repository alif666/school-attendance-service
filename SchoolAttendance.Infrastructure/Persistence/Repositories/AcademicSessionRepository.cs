using Microsoft.EntityFrameworkCore;
using Npgsql;
using SchoolAttendance.Application.AcademicSessions;
using SchoolAttendance.Domain.AcademicSessions;

namespace SchoolAttendance.Infrastructure.Persistence.Repositories;

public class AcademicSessionRepository : IAcademicSessionRepository
{
    private readonly AttendanceDbContext _dbContext;
    public AcademicSessionRepository(AttendanceDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task AddAsync(AcademicSession academicSession, CancellationToken cancellationToken = default)
    {
        await _dbContext.AcademicSessions.AddAsync(academicSession, cancellationToken);
        try
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException exception)
            when (
                exception.InnerException is PostgresException postgresException &&
                postgresException.SqlState == PostgresErrorCodes.UniqueViolation &&
                postgresException.ConstraintName == "IX_AcademicSessions_BranchId_Name"
            )
        {
            throw new DuplicateAcademicSessionNameException(academicSession.BranchId, academicSession.Name);
        }
    }

    public async Task<IReadOnlyList<AcademicSession>> GetByBranchIdAsync(Guid branchId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.AcademicSessions.AsNoTracking().Where(academicSession => academicSession.BranchId == branchId && academicSession.IsActive).OrderBy(academicSession => academicSession.Name).ToListAsync(cancellationToken);
    }

    public async Task<AcademicSession?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.AcademicSessions.AsNoTracking().Where(academicSession => academicSession.IsActive).FirstOrDefaultAsync(academicSession => academicSession.Id == id, cancellationToken);
    }

    public async Task UpdateAsync(AcademicSession academicSession, CancellationToken cancellationToken = default)
    {
        _dbContext.AcademicSessions.Update(academicSession);
        try
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException exception)
            when (
                exception.InnerException is PostgresException postgresException &&
                postgresException.SqlState == PostgresErrorCodes.UniqueViolation &&
                postgresException.ConstraintName == "IX_AcademicSessions_BranchId_Name"
            )
        {
            throw new DuplicateAcademicSessionNameException(academicSession.BranchId, academicSession.Name);
        }
    }
}