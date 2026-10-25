using SchoolAttendance.Domain.Sections;

namespace SchoolAttendance.Application.Sections;

public interface ISectionRepository
{
    Task AddAsync(
        Section section,
        CancellationToken cancellationToken = default
    );
    Task<IReadOnlyList<Section>> GetBySchoolClassIdAsync(
        Guid schoolClassId,
        CancellationToken cancellationToken = default
    );
    Task<Section?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default
    );
    Task UpdateAsync(Section section, CancellationToken cancellationToken = default);
}