namespace SchoolAttendance.Application.AcademicSessions;

public sealed class DuplicateAcademicSessionNameException : Exception
{
    public DuplicateAcademicSessionNameException(Guid branchId, string name) : base($"Branch id '{branchId}' already has '{name}' academic session.")
    {
    }
}