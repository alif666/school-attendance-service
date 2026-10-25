using SchoolAttendance.Domain.SchoolClasses;

namespace SchoolAttendance.Application.SchoolClasses;

public interface ISchoolClassRepository
{
    Task AddAsync(
        SchoolClass schoolClass,
        CancellationToken cancellationToken = default
    );
    Task<IReadOnlyList<SchoolClass>> GetByBranchIdAsync(
        Guid branchId,
        CancellationToken cancellationToken = default
    );
    Task<SchoolClass?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default
    );
    Task UpdateAsync(SchoolClass schoolClass, CancellationToken cancellationToken = default);
}