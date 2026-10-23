namespace SchoolAttendance.Application.Students;

public sealed class DuplicateStudentCodeException : Exception
{
    public DuplicateStudentCodeException(string studentCode) : base($"Student code '{studentCode}' already exists.")
    {
    }
}