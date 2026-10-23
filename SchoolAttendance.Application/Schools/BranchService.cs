using SchoolAttendance.Domain.Schools;

namespace SchoolAttendance.Application.Schools;

public class BranchService : IBranchService
{
    private readonly IBranchRepository _branchRepository;
    public BranchService(IBranchRepository branchRepository)
    {
        _branchRepository = branchRepository;
    }

    public async Task<Branch> CreateBranchAsync(Guid schoolId, string branchCode, string branchName, string? address, CancellationToken cancellationToken = default)
    {
        var branch = new Branch(
            schoolId,
            branchCode,
            branchName,
            address
        );
        await _branchRepository.AddAsync(branch, cancellationToken);
        return branch;
    }

    public async Task<bool> DeactivateBranchAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var branch = await _branchRepository.GetByIdAsync(id, cancellationToken);
        if (branch is null)
        {
            return false;
        }
        branch.Deactivate();
        await _branchRepository.UpdateAsync(branch, cancellationToken);
        return true;
    }

    public async Task<Branch?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var branch = await _branchRepository.GetByIdAsync(
            id,
            cancellationToken
        );
        if (branch is null)
        {
            return null;
        }
        return branch;
    }

    public async Task<IReadOnlyList<Branch>> GetBySchoolIdAsync(Guid schoolId, CancellationToken cancellationToken = default)
    {
        return await _branchRepository.GetBySchoolIdAsync(schoolId, cancellationToken);
    }

    public async Task<Branch?> UpdateBranchAsync(Guid id, string branchName, string? address, CancellationToken cancellationToken = default)
    {
        var branch = await _branchRepository.GetByIdAsync(id, cancellationToken);
        if (branch is null)
        {
            return null;
        }
        branch.UpdateBranchProfile(branchName, address);
        await _branchRepository.UpdateAsync(branch, cancellationToken);
        return branch;
    }
}