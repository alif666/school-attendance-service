using SchoolAttendance.Domain.AcademicSessions;

namespace SchoolAttendance.Application.AcademicSessions;

public class AcademicSessionService : IAcademicSessionService
{
    private readonly IAcademicSessionRepository _academicSessionRepository;
    public AcademicSessionService(IAcademicSessionRepository academicSessionRepository)
    {
        _academicSessionRepository = academicSessionRepository;
    }
    public async Task<AcademicSession> CreateAcademicSessionAsync(Guid branchId, string name, DateOnly startDate, DateOnly endDate, CancellationToken cancellationToken = default)
    {
        var academicSession = new AcademicSession(
            branchId,
            name,
            startDate,
            endDate
        );
        await _academicSessionRepository.AddAsync(academicSession, cancellationToken);
        return academicSession;
    }

    public async Task<bool> DeactivateAcademicSessionAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var academicSession = await _academicSessionRepository.GetByIdAsync(id, cancellationToken);
        if (academicSession is null)
        {
            return false;
        }
        academicSession.Deactivate();
        await _academicSessionRepository.UpdateAsync(academicSession, cancellationToken);
        return true;
    }

    public async Task<IReadOnlyList<AcademicSession>> GetByBranchIdAsync(Guid branchId, CancellationToken cancellationToken = default)
    {
        return await _academicSessionRepository.GetByBranchIdAsync(branchId, cancellationToken);
    }

    public async Task<AcademicSession?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var academicSession = await _academicSessionRepository.GetByIdAsync(id, cancellationToken);
        if (academicSession is null)
        {
            return null;
        }
        return academicSession;
    }

    public async Task<AcademicSession?> UpdateAcademicSessionAsync(Guid id, string name, DateOnly startDate, DateOnly endDate, CancellationToken cancellationToken = default)
    {
        var academicSession = await _academicSessionRepository.GetByIdAsync(id, cancellationToken);
        if (academicSession is null)
        {
            return null;
        }
        academicSession.UpdateAcademicSession(
            name,
            startDate,
            endDate
        );
        await _academicSessionRepository.UpdateAsync(academicSession, cancellationToken);
        return academicSession;
    }
}