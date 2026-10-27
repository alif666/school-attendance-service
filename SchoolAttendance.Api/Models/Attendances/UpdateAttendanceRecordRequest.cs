using System.ComponentModel.DataAnnotations;
using SchoolAttendance.Domain.Attendances;

namespace SchoolAttendance.Api.Models.Attendances;
public class UpdateAttendanceRecordRequest
{
    [Required]
    public AttendanceStatus Status { get; init; }
}