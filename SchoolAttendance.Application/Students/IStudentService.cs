using SchoolAttendance.Domain.Students;

namespace SchoolAttendance.Application.Students;

public interface IStudentService
{
    Task<Student> CreateStudentAsync(string studentCode, string fullName, DateOnly? dateOfBirth = null, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Student>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Student?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Student?> UpdateStudentAsync(Guid id, string fullName, DateOnly? dateOfBirth = null, CancellationToken cancellationToken = default);

    Task<bool> DeactivateStudentAsync(Guid id, CancellationToken cancellationToken = default);
}