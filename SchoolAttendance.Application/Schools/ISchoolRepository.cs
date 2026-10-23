using SchoolAttendance.Domain.Schools;

namespace SchoolAttendance.Application.Schools;

public interface ISchoolRepository
{
    Task AddAsync(
        School school,
        CancellationToken cancellationToken = default
    );
    Task<IReadOnlyList<School>> GetAllAsync(
        CancellationToken cancellationToken = default
    );
    Task<School?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken
    );
    Task UpdateAsync(
        School school,
        CancellationToken cancellationToken = default
    );
}