namespace SchoolAttendance.Application.Sections;

public sealed class DuplicateSectionNameException : Exception
{
    public DuplicateSectionNameException(Guid schoolClassId, string name) : base($"School Class id '{schoolClassId}' already has '{name}' section.")
    {
    }
}