namespace SchoolAttendance.Application.SchoolClasses;

public sealed class DuplicateSchoolClassNameException : Exception
{
    public DuplicateSchoolClassNameException(Guid branchId, string name) : base($"Branch id '{branchId}' already has '{name}' school class.")
    {
    }
}