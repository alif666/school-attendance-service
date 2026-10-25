namespace SchoolAttendance.Domain.Sections;

public class Section
{
    public Guid Id { get; private set; }
    public Guid SchoolClassId { get; private set; }
    public string Name { get; private set; }
    public bool IsActive { get; private set; } = true;
    public Section(Guid schoolClassId, string name)
    {
        Id = Guid.NewGuid();
        SchoolClassId = schoolClassId;
        Name = ValidateSectionName(name);
    }
    public void ChangeSectionName(string name)
    {
        Name = ValidateSectionName(name);
    }
    public void Deactivate()
    {
        IsActive = false;
    }
    private static string ValidateSectionName(string sectionName)
    {
        if (string.IsNullOrWhiteSpace(sectionName))
        {
            throw new ArgumentException("Section name cannot be empty.", nameof(sectionName));
        }
        return sectionName;
    }
}