using SchoolAttendance.Domain.Enrollments;

namespace SchoolAttendance.Application.Enrollments;

public interface IEnrollmentRepository
{
    Task AddAsync(
        Enrollment enrollment,
        CancellationToken cancellationToken = default
    );
    Task<IReadOnlyList<Enrollment>> GetBySectionIdAndAcademicSessionIdAsync(
        Guid sectionId,
        Guid academicSessionId,
        CancellationToken cancellationToken = default
    );
    Task<Enrollment?> GetByStudentIdAndAcademicSessionIdAsync(
        Guid studentId,
        Guid academicSessionId,
        CancellationToken cancellationToken = default
    );
    Task<Enrollment?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default
    );
    Task UpdateAsync(Enrollment enrollment, CancellationToken cancellationToken = default);
}