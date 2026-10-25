using Microsoft.EntityFrameworkCore;
using Npgsql;
using SchoolAttendance.Application.SchoolClasses;
using SchoolAttendance.Domain.SchoolClasses;

namespace SchoolAttendance.Infrastructure.Persistence.Repositories;

public class SchoolClassRepository : ISchoolClassRepository
{
    private readonly AttendanceDbContext _dbContext;
    public SchoolClassRepository(AttendanceDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task AddAsync(SchoolClass schoolClass, CancellationToken cancellationToken = default)
    {
        await _dbContext.SchoolClasses.AddAsync(schoolClass, cancellationToken);
        try
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException exception)
            when (
                exception.InnerException is PostgresException postgresException &&
                postgresException.SqlState == PostgresErrorCodes.UniqueViolation &&
                postgresException.ConstraintName == "IX_SchoolClasses_BranchId_Name"
            )
        {
            throw new DuplicateSchoolClassNameException(schoolClass.BranchId, schoolClass.Name);
        }
    }

    public async Task<IReadOnlyList<SchoolClass>> GetByBranchIdAsync(Guid branchId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.SchoolClasses.AsNoTracking().Where(schoolClass => schoolClass.BranchId == branchId && schoolClass.IsActive).OrderBy(schoolClass => schoolClass.Name).ToListAsync(cancellationToken);

    }

    public async Task<SchoolClass?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.SchoolClasses.AsNoTracking().Where(schoolClass => schoolClass.IsActive).FirstOrDefaultAsync(schoolClass => schoolClass.Id == id, cancellationToken);
    }

    public async Task UpdateAsync(SchoolClass schoolClass, CancellationToken cancellationToken = default)
    {
        _dbContext.SchoolClasses.Update(schoolClass);
        try
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException exception)
            when (
                exception.InnerException is PostgresException postgresException &&
                postgresException.SqlState == PostgresErrorCodes.UniqueViolation &&
                postgresException.ConstraintName == "IX_SchoolClasses_BranchId_Name"
            )
        {
            throw new DuplicateSchoolClassNameException(schoolClass.BranchId, schoolClass.Name);
        }
    }
}