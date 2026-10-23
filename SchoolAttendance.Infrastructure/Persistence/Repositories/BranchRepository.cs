using Microsoft.EntityFrameworkCore;
using Npgsql;
using SchoolAttendance.Application.Schools;
using SchoolAttendance.Domain.Schools;

namespace SchoolAttendance.Infrastructure.Persistence.Repositories;

public class BranchRepository : IBranchRepository
{
    private readonly AttendanceDbContext _dbContext;
    public BranchRepository(AttendanceDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task AddAsync(Branch branch, CancellationToken cancellationToken = default)
    {
        await _dbContext.Branches.AddAsync(branch, cancellationToken);
        try
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException exception)
            when (
                exception.InnerException is PostgresException postgresException &&
                postgresException.SqlState == PostgresErrorCodes.UniqueViolation &&
                postgresException.ConstraintName == "IX_Branches_SchoolId_BranchCode"
            )
        {
            throw new DuplicateBranchCodeException(branch.SchoolId, branch.BranchCode);
        }
    }

    public async Task<Branch?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Branches.AsNoTracking().Where(branch => branch.IsActive).FirstOrDefaultAsync(branch => branch.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Branch>> GetBySchoolIdAsync(Guid schoolId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Branches.AsNoTracking().Where(branch => branch.IsActive && branch.SchoolId == schoolId).OrderBy(branch => branch.BranchName).ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(Branch branch, CancellationToken cancellationToken = default)
    {
        _dbContext.Branches.Update(branch);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}