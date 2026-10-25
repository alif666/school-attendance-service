namespace SchoolAttendance.Domain.AcademicSessions;

public class AcademicSession
{
    public Guid Id { get; private set; }
    public Guid BranchId { get; private set; }
    public string Name { get; private set; }
    public DateOnly StartDate { get; private set; }
    public DateOnly EndDate { get; private set; }
    public bool IsActive { get; private set; } = true;

    public AcademicSession(
        Guid branchId,
        string name,
        DateOnly startDate,
        DateOnly endDate
    )
    {

        ValidateDateRange(startDate, endDate);
        Id = Guid.NewGuid();
        BranchId = branchId;
        Name = ValidateName(name);
        StartDate = startDate;
        EndDate = endDate;
    }
    public void ChangeName(string name)
    {
        Name = ValidateName(name);
    }
    public void UpdateAcademicSession(string name, DateOnly startDate, DateOnly endDate)
    {
        ValidateDateRange(startDate, endDate);
        Name = ValidateName(name);
        StartDate = startDate;
        EndDate = endDate;
    }
    public void Deactivate()
    {
        IsActive = false;
    }
    private static string ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Academic session name cannot be empty.", nameof(name));
        }
        return name;
    }
    private static DateOnly ValidateDateRange(DateOnly startDate, DateOnly endDate)
    {
        if (endDate < startDate)
        {
            throw new ArgumentException("End date cannot be earlier than start date.", nameof(endDate));
        }
        return endDate;
    }
}