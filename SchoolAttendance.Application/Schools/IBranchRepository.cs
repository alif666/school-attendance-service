using SchoolAttendance.Domain.Schools;

namespace SchoolAttendance.Application.Schools;

public interface IBranchRepository
{
    Task AddAsync(Branch branch, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Branch>> GetBySchoolIdAsync(Guid schoolId, CancellationToken cancellationToken = default);
    Task<Branch?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task UpdateAsync(Branch branch, CancellationToken cancellationToken = default);
}