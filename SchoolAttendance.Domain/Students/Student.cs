namespace SchoolAttendance.Domain.Students;

public class Student
{
    public Guid Id { get; private set; }

    public string StudentCode { get; private set; }

    public string FullName { get; private set; }

    public DateOnly? DateOfBirth { get; private set; }

    public bool IsActive { get; private set; } = true;

    public void Deactivate()
    {
        IsActive = false;
    }

    public Student(
        string studentCode,
        string fullName,
        DateOnly? dateOfBirth = null
    )
    {
        Id = Guid.NewGuid();
        StudentCode = studentCode;
        FullName = fullName;
        DateOfBirth = dateOfBirth;
    }

    public void ChangeFullName(string fullName)
    {
        FullName = ValidateFullName(fullName);
    }

    public void UpdateProfile(string fullName, DateOnly? dateOfBirth)
    {
        FullName = ValidateFullName(fullName);
        DateOfBirth = dateOfBirth;
    }

    private static string ValidateFullName(string fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName))
        {
            throw new ArgumentException("Full name cannot be empty.", nameof(fullName));
        }
        return fullName;
    }

}