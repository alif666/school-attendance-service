namespace SchoolAttendance.Domain.Attendances;

public class AttendanceRecord
{
    public Guid Id { get; private set; }
    public Guid EnrollmentId { get; private set; }
    public DateOnly AttendanceDate { get; private set; }
    public AttendanceEventType EventType { get; private set; }
    public AttendanceStatus Status { get; private set; }

    public AttendanceRecord(
        Guid enrollmentId,
        DateOnly attendanceDate,
        AttendanceEventType eventType,
        AttendanceStatus status
    )
    {
        if (enrollmentId == Guid.Empty)
        {
            throw new ArgumentException("Enrollment ID is required.", nameof(enrollmentId));
        }
        if (attendanceDate == DateOnly.MinValue)
        {
            throw new ArgumentException("Attendance date is required.", nameof(attendanceDate));
        }
        if (!Enum.IsDefined(eventType))
        {
            throw new ArgumentException("Invalid attendance event type.", nameof(eventType));
        }
        if (!Enum.IsDefined(status))
        {
            throw new ArgumentException("Invalid attendance status.", nameof(status));
        }
        Id = Guid.NewGuid();
        EnrollmentId = enrollmentId;
        AttendanceDate = attendanceDate;
        EventType = eventType;
        Status = status;
    }
    public void ChangeStatus(AttendanceStatus status)
    {
        if (!Enum.IsDefined(status))
        {
            throw new ArgumentException("Invalid attendance status.", nameof(status));
        }
        Status = status;
    }
}