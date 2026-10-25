using SchoolAttendance.Domain.AcademicSessions;

namespace SchoolAttendance.Application.AcademicSessions;

public interface IAcademicSessionRepository
{
    Task AddAsync(
        AcademicSession academicSession,
        CancellationToken cancellationToken = default
    );
    Task<IReadOnlyList<AcademicSession>> GetByBranchIdAsync(
        Guid branchId,
        CancellationToken cancellationToken = default
    );
    Task<AcademicSession?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default
    );
    Task UpdateAsync(AcademicSession academicSession, CancellationToken cancellationToken = default);
}