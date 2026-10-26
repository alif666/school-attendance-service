namespace SchoolAttendance.Application.Enrollments;

public sealed class DuplicateEnrollmentException : Exception
{
    public DuplicateEnrollmentException(
        Guid studentId,
        Guid academicSessionId
    ) : base($"Student '{studentId}' is already enrolled in academic session '{academicSessionId}'.")
    {
        StudentId = studentId;
        AcademicSessionId = academicSessionId;
    }
    public Guid StudentId { get; }
    public Guid AcademicSessionId { get; }
}