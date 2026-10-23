namespace SchoolAttendance.Domain.Schools;

public class Branch
{
    public Guid Id { get; private set; }
    public Guid SchoolId { get; private set; }
    public string BranchCode { get; private set; }
    public string BranchName { get; private set; }
    public string? Address { get; private set; }
    public bool IsActive { get; private set; } = true;
    public Branch(Guid schoolId, string branchCode, string branchName, string? address = null)
    {
        Id = Guid.NewGuid();
        SchoolId = schoolId;
        BranchCode = branchCode;
        BranchName = ValidateBranchName(branchName);
        Address = address;
    }
    public void ChangeBranchName(string branchName)
    {
        BranchName = ValidateBranchName(branchName);
    }
    public void Deactivate()
    {
        IsActive = false;
    }
    private static string ValidateBranchName(string branchName)
    {
        if (string.IsNullOrWhiteSpace(branchName))
        {
            throw new ArgumentException("Branch name cannot be empty.", nameof(branchName));
        }
        return branchName;
    }
}