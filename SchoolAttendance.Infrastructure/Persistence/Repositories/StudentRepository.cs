using Microsoft.EntityFrameworkCore;
using Npgsql;
using SchoolAttendance.Application.Students;
using SchoolAttendance.Domain.Students;

namespace SchoolAttendance.Infrastructure.Persistence.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly AttendanceDbContext _dbContext;
    public StudentRepository(AttendanceDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task AddAsync(Student student, CancellationToken cancellationToken = default)
    {
        await _dbContext.Students.AddAsync(
            student,
            cancellationToken);
        try
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException exception)
            when (
                exception.InnerException is PostgresException postgresException &&
                postgresException.SqlState == PostgresErrorCodes.UniqueViolation &&
                postgresException.ConstraintName == "IX_Students_StudentCode")
        {
            throw new DuplicateStudentCodeException(student.StudentCode);
        }

    }

    public async Task<IReadOnlyList<Student>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Students.AsNoTracking().Where(student => student.IsActive).OrderBy(student => student.FullName).ToListAsync(cancellationToken);
    }
    public async Task<Student?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Students.AsNoTracking().Where(student => student.IsActive).FirstOrDefaultAsync(student => student.Id == id, cancellationToken);
    }

    public async Task UpdateAsync(Student student, CancellationToken cancellationToken = default)
    {
        _dbContext.Students.Update(student);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}