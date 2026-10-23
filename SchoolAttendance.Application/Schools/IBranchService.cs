using SchoolAttendance.Domain.Schools;

namespace SchoolAttendance.Application.Schools;

public interface IBranchService
{
    Task<Branch> CreateBranchAsync(Guid schoolId, string branchCode, string branchName, string? address, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Branch>> GetBySchoolIdAsync(Guid schoolId, CancellationToken cancellationToken = default);
    Task<Branch?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Branch?> UpdateBranchAsync(Guid id, string branchName, string? address, CancellationToken cancellationToken = default);
    Task<bool> DeactivateBranchAsync(Guid id, CancellationToken cancellationToken = default);
}