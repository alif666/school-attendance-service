namespace SchoolAttendance.Application.Enrollments;

public sealed class EnrollmentNotFoundException : Exception
{
    public EnrollmentNotFoundException(Guid enrollmentId)
        : base($"Enrollment with ID '{enrollmentId}' was not found.")
    {
    }
}
