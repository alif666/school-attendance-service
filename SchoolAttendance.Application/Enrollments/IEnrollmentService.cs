using SchoolAttendance.Domain.Enrollments;

namespace SchoolAttendance.Application.Enrollments;

public interface IEnrollmentService
{
    Task<Enrollment> CreateEnrollmentAsync(Guid studentId, Guid academicSessionId, Guid sectionId, DateOnly enrollmentDate, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Enrollment>> GetBySectionIdAndAcademicSessionIdAsync(Guid sectionId, Guid academicSessionId, CancellationToken cancellationToken = default);
    Task<Enrollment?> GetByStudentIdAndAcademicSessionIdAsync(Guid studentId, Guid academicSessionId, CancellationToken cancellationToken = default);
    Task<Enrollment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Enrollment?> UpdateEnrollmentAsync(Guid id, Guid sectionId, CancellationToken cancellationToken = default);
    Task<bool> DeactivateEnrollmentAsync(Guid id, CancellationToken cancellationToken = default);
}