using SchoolAttendance.Domain.Schools;

namespace SchoolAttendance.Application.Schools;

public interface ISchoolService
{
    Task<School> CreateSchoolAsync(string schoolCode, string schoolName, CancellationToken cancellationToken);
    Task<IReadOnlyList<School>> GetAllAsync(CancellationToken cancellationToken);
    Task<School?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<School?> UpdateSchoolAsync(Guid id, string schoolName, CancellationToken cancellationToken = default);
    Task<bool> DeactivateSchoolAsync(Guid id, CancellationToken cancellationToken = default);
}