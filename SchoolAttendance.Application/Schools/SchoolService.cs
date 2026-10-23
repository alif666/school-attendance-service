using SchoolAttendance.Domain.Schools;

namespace SchoolAttendance.Application.Schools;

public class SchoolService : ISchoolService
{
    private readonly ISchoolRepository _schoolRepository;
    public SchoolService(ISchoolRepository schoolRepository)
    {
        _schoolRepository = schoolRepository;
    }
    public async Task<School> CreateSchoolAsync(string schoolCode, string schoolName, CancellationToken cancellationToken = default)
    {
        var school = new School(
            schoolCode,
            schoolName
        );
        await _schoolRepository.AddAsync(school, cancellationToken);
        return school;
    }

    public async Task<bool> DeactivateSchoolAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var school = await _schoolRepository.GetByIdAsync(id, cancellationToken);
        if (school is null)
        {
            return false;
        }
        school.Deactivate();
        await _schoolRepository.UpdateAsync(school, cancellationToken);
        return true;
    }

    public async Task<IReadOnlyList<School>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _schoolRepository.GetAllAsync(cancellationToken);
    }

    public async Task<School?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _schoolRepository.GetByIdAsync(id, cancellationToken);
    }

    public async Task<School?> UpdateSchoolAsync(Guid id, string schoolName, CancellationToken cancellationToken = default)
    {
        var school = await _schoolRepository.GetByIdAsync(id, cancellationToken);
        if (school is null)
        {
            return null;
        }
        school.ChangeSchoolName(schoolName);
        await _schoolRepository.UpdateAsync(school, cancellationToken);
        return school;
    }
}