namespace SchoolAttendance.Domain.Schools;

public class School
{
    public Guid Id { get; private set; }

    public string SchoolCode { get; private set; }

    public string SchoolName { get; private set; }

    public bool IsActive { get; private set; } = true;

    public void Deactivate()
    {
        IsActive = false;
    }

    public School(
        string schoolCode,
        string schoolName
    )
    {
        Id = Guid.NewGuid();
        SchoolCode = schoolCode;
        SchoolName = ValidateSchoolName(schoolName);
    }

    public void ChangeSchoolName(string schoolName)
    {
        SchoolName = ValidateSchoolName(schoolName);
    }
    private static string ValidateSchoolName(string schoolName)
    {
        if (string.IsNullOrWhiteSpace(schoolName))
        {
            throw new ArgumentException("School name cannot be empty.", nameof(schoolName));
        }
        return schoolName;
    }
}