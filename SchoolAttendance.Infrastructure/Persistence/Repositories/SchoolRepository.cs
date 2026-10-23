using Microsoft.EntityFrameworkCore;
using Npgsql;
using SchoolAttendance.Application.Schools;
using SchoolAttendance.Domain.Schools;

namespace SchoolAttendance.Infrastructure.Persistence.Repositories;

public class SchoolRepository : ISchoolRepository
{
    private readonly AttendanceDbContext _dbContext;
    public SchoolRepository(AttendanceDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task AddAsync(School school, CancellationToken cancellationToken = default)
    {
        await _dbContext.Schools.AddAsync(
            school,
            cancellationToken
        );
        try
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException exception)
            when (
                exception.InnerException is PostgresException postgresException &&
                postgresException.SqlState == PostgresErrorCodes.UniqueViolation &&
                postgresException.ConstraintName == "IX_Schools_SchoolCode")
        {
            throw new DuplicateSchoolCodeException(school.SchoolCode);
        }
    }
    public async Task<IReadOnlyList<School>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Schools.AsNoTracking().Where(school => school.IsActive).OrderBy(school => school.SchoolName).ToListAsync(cancellationToken);
    }
    public async Task<School?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Schools.AsNoTracking().Where(school => school.IsActive).FirstOrDefaultAsync(school => school.Id == id, cancellationToken);
    }
    public async Task UpdateAsync(School school, CancellationToken cancellationToken = default)
    {
        _dbContext.Schools.Update(school);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}