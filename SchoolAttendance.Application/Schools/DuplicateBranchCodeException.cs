namespace SchoolAttendance.Application.Schools;

public sealed class DuplicateBranchCodeException : Exception
{
    public DuplicateBranchCodeException(Guid schoolId, string branchCode) :base($"Branch code '{branchCode}' already exists in school '{schoolId}'.")
    {
    }
}