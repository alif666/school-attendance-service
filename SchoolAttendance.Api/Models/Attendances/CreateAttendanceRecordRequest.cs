using System.ComponentModel.DataAnnotations;
using SchoolAttendance.Domain.Attendances;

namespace SchoolAttendance.Api.Models.Attendances;

public class CreateAttendanceRecordRequest
{
    [Required]
    public Guid EnrollmentId { get; set; }
    [Required]
    public DateOnly AttendanceDate { get; set; }
    [Required]
    public AttendanceEventType EventType { get; set; }
    [Required]
    public AttendanceStatus Status { get; set; }
}