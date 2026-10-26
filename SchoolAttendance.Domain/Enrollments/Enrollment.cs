namespace SchoolAttendance.Domain.Enrollments;

public class Enrollment
{
    public Guid Id { get; private set; }
    public Guid StudentId { get; private set; }
    public Guid AcademicSessionId { get; private set; }
    public Guid SectionId { get; private set; }
    public DateOnly EnrollmentDate { get; private set; }
    public bool IsActive { get; private set; } = true;
    public Enrollment(Guid studentId, Guid academicSessionId, Guid sectionId, DateOnly enrollmentDate)
    {
        if (studentId == Guid.Empty) throw new ArgumentException("Student ID is required.", nameof(studentId));
        if (academicSessionId == Guid.Empty) throw new ArgumentException("Academic session ID is required.", nameof(academicSessionId));
        if (sectionId == Guid.Empty) throw new ArgumentException("Section ID is required.", nameof(sectionId));
        if (enrollmentDate == DateOnly.MinValue) throw new ArgumentException("Enrollment date is required", nameof(enrollmentDate));
        Id = Guid.NewGuid();
        StudentId = studentId;
        AcademicSessionId = academicSessionId;
        SectionId = sectionId;
        EnrollmentDate = enrollmentDate;
    }
    public void Deactivate()
    {
        IsActive = false;
    }
    public void ChangeSection(Guid sectionId)
    {
        if (sectionId == Guid.Empty) throw new ArgumentException("Section ID is required.", nameof(sectionId));
        SectionId = sectionId;
    }
}