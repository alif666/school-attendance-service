using SchoolAttendance.Domain.SchoolClasses;

namespace SchoolAttendance.Application.SchoolClasses;

public interface ISchoolClassService
{
    Task<SchoolClass> CreateSchoolClassAsync(Guid branchId, string name, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<SchoolClass>> GetByBranchIdAsync(Guid branchId, CancellationToken cancellationToken = default);
    Task<SchoolClass?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<SchoolClass?> UpdateSchoolClassAsync(Guid id, string name, CancellationToken cancellationToken = default);
    Task<bool> DeactivateSchoolClassAsync(Guid id, CancellationToken cancellationToken = default);
}