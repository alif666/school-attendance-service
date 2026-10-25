using SchoolAttendance.Domain.SchoolClasses;

namespace SchoolAttendance.Application.SchoolClasses;

public class SchoolClassService : ISchoolClassService
{
    private readonly ISchoolClassRepository _schoolClassRepository;
    public SchoolClassService(ISchoolClassRepository schoolClassRepository)
    {
        _schoolClassRepository = schoolClassRepository;
    }
    public async Task<SchoolClass> CreateSchoolClassAsync(Guid branchId, string name, CancellationToken cancellationToken = default)
    {
        var schoolClass = new SchoolClass(
            branchId,
            name
        );
        await _schoolClassRepository.AddAsync(schoolClass, cancellationToken);
        return schoolClass;
    }

    public async Task<bool> DeactivateSchoolClassAsync(Guid id, CancellationToken cancellationToken)
    {
        var schoolClass = await _schoolClassRepository.GetByIdAsync(id, cancellationToken);
        if (schoolClass is null)
        {
            return false;
        }
        schoolClass.Deactivate();
        await _schoolClassRepository.UpdateAsync(schoolClass, cancellationToken);
        return true;
    }

    public async Task<IReadOnlyList<SchoolClass>> GetByBranchIdAsync(Guid branchId, CancellationToken cancellationToken = default)
    {
        return await _schoolClassRepository.GetByBranchIdAsync(branchId, cancellationToken);
    }

    public async Task<SchoolClass?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var schoolClass = await _schoolClassRepository.GetByIdAsync(id, cancellationToken);
        if (schoolClass is null)
        {
            return null;
        }
        return schoolClass;
    }

    public async Task<SchoolClass?> UpdateSchoolClassAsync(Guid id, string name, CancellationToken cancellationToken = default)
    {
        var schoolClass = await _schoolClassRepository.GetByIdAsync(id, cancellationToken);
        if (schoolClass is null)
        {
            return null;
        }
        schoolClass.ChangeSchoolClassName(name);
        await _schoolClassRepository.UpdateAsync(schoolClass, cancellationToken);
        return schoolClass;
    }
}