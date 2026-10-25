using SchoolAttendance.Domain.AcademicSessions;

namespace SchoolAttendance.Application.AcademicSessions;

public interface IAcademicSessionService
{
    Task<AcademicSession> CreateAcademicSessionAsync(Guid branchId, string name, DateOnly startDate, DateOnly endDate, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<AcademicSession>> GetByBranchIdAsync(Guid branchId, CancellationToken cancellationToken = default);
    Task<AcademicSession?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<AcademicSession?> UpdateAcademicSessionAsync(Guid id, string name, DateOnly startDate, DateOnly endDate, CancellationToken cancellationToken = default);
    Task<bool> DeactivateAcademicSessionAsync(Guid id, CancellationToken cancellationToken = default);
}