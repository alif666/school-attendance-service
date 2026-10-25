using SchoolAttendance.Domain.Sections;

namespace SchoolAttendance.Application.Sections;

public class SectionService : ISectionService
{
    private readonly ISectionRepository _sectionRepository;
    public SectionService(ISectionRepository sectionRepository)
    {
        _sectionRepository = sectionRepository;
    }
    public async Task<Section> CreateSectionAsync(Guid schoolClassId, string name, CancellationToken cancellationToken = default)
    {
        var section = new Section(
            schoolClassId,
            name
        );
        await _sectionRepository.AddAsync(section, cancellationToken);
        return section;
    }

    public async Task<bool> DeactivateSectionAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var section = await _sectionRepository.GetByIdAsync(id, cancellationToken);
        if (section is null)
        {
            return false;
        }
        section.Deactivate();
        await _sectionRepository.UpdateAsync(section, cancellationToken);
        return true;
    }

    public async Task<Section?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var section = await _sectionRepository.GetByIdAsync(id, cancellationToken);
        if (section is null)
        {
            return null;
        }
        return section;
    }

    public async Task<IReadOnlyList<Section>> GetBySchoolClassIdAsync(Guid schoolClassId, CancellationToken cancellationToken = default)
    {
        return await _sectionRepository.GetBySchoolClassIdAsync(schoolClassId, cancellationToken);
    }

    public async Task<Section?> UpdateSectionAsync(Guid id, string name, CancellationToken cancellationToken = default)
    {
        var section = await _sectionRepository.GetByIdAsync(id, cancellationToken);
        if (section is null)
        {
            return null;
        }
        section.ChangeSectionName(name);
        await _sectionRepository.UpdateAsync(section, cancellationToken);
        return section;
    }
}