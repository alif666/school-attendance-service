using SchoolAttendance.Domain.Enrollments;

namespace SchoolAttendance.Application.Enrollments;

public class EnrollmentService : IEnrollmentService
{
    private readonly IEnrollmentRepository _enrollmentRepository;
    public EnrollmentService(IEnrollmentRepository enrollmentRepository)
    {
        _enrollmentRepository = enrollmentRepository;
    }
    public async Task<Enrollment> CreateEnrollmentAsync(Guid studentId, Guid academicSessionId, Guid sectionId, DateOnly enrollmentDate, CancellationToken cancellationToken = default)
    {
        var enrollment = new Enrollment(
            studentId,
            academicSessionId,
            sectionId,
            enrollmentDate
        );
        await _enrollmentRepository.AddAsync(enrollment, cancellationToken);
        return enrollment;
    }

    public async Task<bool> DeactivateEnrollmentAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var enrollment = await _enrollmentRepository.GetByIdAsync(id, cancellationToken);
        if (enrollment is null)
        {
            return false;
        }
        enrollment.Deactivate();
        await _enrollmentRepository.UpdateAsync(enrollment, cancellationToken);
        return true;
    }

    public async Task<Enrollment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var enrollment = await _enrollmentRepository.GetByIdAsync(id, cancellationToken);
        if (enrollment is null)
        {
            return null;
        }
        return enrollment;
    }

    public async Task<IReadOnlyList<Enrollment>> GetBySectionIdAndAcademicSessionIdAsync(Guid sectionId, Guid academicSessionId, CancellationToken cancellationToken = default)
    {
        return await _enrollmentRepository.GetBySectionIdAndAcademicSessionIdAsync(sectionId, academicSessionId, cancellationToken);
    }

    public async Task<Enrollment?> GetByStudentIdAndAcademicSessionIdAsync(Guid studentId, Guid academicSessionId, CancellationToken cancellationToken = default)
    {
        var enrollment = await _enrollmentRepository.GetByStudentIdAndAcademicSessionIdAsync(studentId, academicSessionId, cancellationToken);
        if (enrollment is null)
        {
            return null;
        }
        return enrollment;
    }

    public async Task<Enrollment?> UpdateEnrollmentAsync(Guid id, Guid sectionId, CancellationToken cancellationToken = default)
    {
        var enrollment = await _enrollmentRepository.GetByIdAsync(id, cancellationToken);
        if (enrollment is null)
        {
            return null;
        }
        enrollment.ChangeSection(sectionId);
        await _enrollmentRepository.UpdateAsync(enrollment, cancellationToken);
        return enrollment;
    }
}