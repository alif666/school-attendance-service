namespace SchoolAttendance.Domain.SchoolClasses;

public class SchoolClass
{
    public Guid Id { get; private set; }
    public Guid BranchId { get; private set; }
    public string Name { get; private set; }
    public bool IsActive { get; private set; } = true;
    public SchoolClass(Guid branchId, string name)
    {
        Id = Guid.NewGuid();
        BranchId = branchId;
        Name = ValidateSchoolClassName(name);
    }
    public void ChangeSchoolClassName(string name)
    {
        Name = ValidateSchoolClassName(name);
    }
    public void Deactivate()
    {
        IsActive = false;
    }
    private static string ValidateSchoolClassName(string schoolClassName)
    {
        if (string.IsNullOrWhiteSpace(schoolClassName))
        {
            throw new ArgumentException("School class name cannot be empty.", nameof(schoolClassName));
        }
        return schoolClassName;
    }
}