using SchoolAttendance.Domain.Sections;

namespace SchoolAttendance.Application.Sections;

public interface ISectionService
{
    Task<Section> CreateSectionAsync(Guid schoolClassId, string name, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Section>> GetBySchoolClassIdAsync(Guid schoolClassId, CancellationToken cancellationToken = default);
    Task<Section?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Section?> UpdateSectionAsync(Guid id, string name, CancellationToken cancellationToken = default);
    Task<bool> DeactivateSectionAsync(Guid id, CancellationToken cancellationToken = default);
}