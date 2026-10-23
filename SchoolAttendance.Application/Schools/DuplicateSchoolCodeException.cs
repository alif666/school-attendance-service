namespace SchoolAttendance.Application.Schools;

public sealed class DuplicateSchoolCodeException : Exception
{
    public DuplicateSchoolCodeException(string schoolCode) : base($"School code '{schoolCode}' already exists.")
    {
    }
}