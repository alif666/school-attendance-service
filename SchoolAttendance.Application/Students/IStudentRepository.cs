using SchoolAttendance.Domain.Students;

namespace SchoolAttendance.Application.Students;

public interface IStudentRepository
{
    Task AddAsync(
        Student student,
        CancellationToken cancellationToken = default
    );

    Task<IReadOnlyList<Student>> GetAllAsync(
        CancellationToken cancellationToken = default
    );

    Task<Student?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default
    );

    Task UpdateAsync(
        Student student,
        CancellationToken cancellationToken = default
    );
}