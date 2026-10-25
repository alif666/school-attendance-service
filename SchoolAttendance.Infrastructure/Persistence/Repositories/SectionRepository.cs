using Microsoft.EntityFrameworkCore;
using Npgsql;
using SchoolAttendance.Application.Sections;
using SchoolAttendance.Domain.Sections;

namespace SchoolAttendance.Infrastructure.Persistence.Repositories;

public class SectionRepository : ISectionRepository
{
    private readonly AttendanceDbContext _dbContext;
    public SectionRepository(AttendanceDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task AddAsync(Section section, CancellationToken cancellationToken = default)
    {
        await _dbContext.Sections.AddAsync(section, cancellationToken);
        try
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException exception)
            when (
                exception.InnerException is PostgresException postgresException &&
                postgresException.SqlState == PostgresErrorCodes.UniqueViolation &&
                postgresException.ConstraintName == "IX_Sections_SchoolClassId_Name"
            )
        {
            throw new DuplicateSectionNameException(section.SchoolClassId, section.Name);
        }
    }

    public async Task<IReadOnlyList<Section>> GetBySchoolClassIdAsync(Guid schoolClassId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Sections.AsNoTracking().Where(section => section.SchoolClassId == schoolClassId && section.IsActive).OrderBy(section => section.Name).ToListAsync(cancellationToken);
    }

    public async Task<Section?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Sections.AsNoTracking().Where(section => section.IsActive).FirstOrDefaultAsync(section => section.Id == id, cancellationToken);
    }

    public async Task UpdateAsync(Section section, CancellationToken cancellationToken = default)
    {
        _dbContext.Update(section);
        try
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException exception)
            when (
                exception.InnerException is PostgresException postgresException &&
                postgresException.SqlState == PostgresErrorCodes.UniqueViolation &&
                postgresException.ConstraintName == "IX_Sections_SchoolClassId_Name"
            )
        {
            throw new DuplicateSectionNameException(section.SchoolClassId, section.Name);
        }
    }
}