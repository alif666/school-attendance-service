using Microsoft.AspNetCore.Mvc;
using SchoolAttendance.Api.Models.Attendances;
using SchoolAttendance.Application.Attendances;
using SchoolAttendance.Domain.Attendances;

namespace SchoolAttendance.Api.Controllers;

[ApiController]
[Route("api/attendance-records")]
public class AttendanceRecordsController : ControllerBase
{
    private readonly IAttendanceRecordService _attendanceRecordService;
    public AttendanceRecordsController(IAttendanceRecordService attendanceRecordService)
    {
        _attendanceRecordService = attendanceRecordService;
    }
    [HttpGet("sections")]
    public async Task<ActionResult<IReadOnlyList<AttendanceRecordResponse>>> GetBySectionIdAndDateRangeAsync(Guid sectionId, Guid academicSessionId, DateOnly fromDate, DateOnly toDate, CancellationToken cancellationToken)
    {
        var attendanceRecords = await _attendanceRecordService.GetBySectionIdAndDateRangeAsync(sectionId, academicSessionId, fromDate, toDate, cancellationToken);
        var response = attendanceRecords.Select(
            attendanceRecord => new AttendanceRecordResponse(
                attendanceRecord.Id,
                attendanceRecord.EnrollmentId,
                attendanceRecord.AttendanceDate,
                attendanceRecord.EventType,
                attendanceRecord.Status
            )).ToList();
        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<AttendanceRecordResponse>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var attendanceRecord = await _attendanceRecordService.GetByIdAsync(id, cancellationToken);
        if (attendanceRecord is null)
        {
            return NotFound();
        }
        var response = new AttendanceRecordResponse(
                attendanceRecord.Id,
                attendanceRecord.EnrollmentId,
                attendanceRecord.AttendanceDate,
                attendanceRecord.EventType,
                attendanceRecord.Status
        );
        return Ok(response);
    }
    [HttpGet("enrollments")]
    public async Task<ActionResult<AttendanceRecordResponse>> GetByEnrollmentIdAndAttendanceDateAndEventType(Guid enrollmentId, DateOnly attendanceDate, AttendanceEventType eventType, CancellationToken cancellationToken)
    {
        var attendanceRecord = await _attendanceRecordService.GetByEnrollmentIdAndAttendanceDateAndEventTypeAsync(enrollmentId, attendanceDate, eventType, cancellationToken);
        if (attendanceRecord is null)
        {
            return NotFound();
        }
        var response = new AttendanceRecordResponse(
            attendanceRecord.Id,
            attendanceRecord.EnrollmentId,
            attendanceRecord.AttendanceDate,
            attendanceRecord.EventType,
            attendanceRecord.Status
        );
        return Ok(response);
    }
    [HttpPost]
    public async Task<ActionResult<AttendanceRecordResponse>> Create(CreateAttendanceRecordRequest request, CancellationToken cancellationToken)
    {
        var attendanceRecord = await _attendanceRecordService.CreateAttendanceRecordAsync(
            request.EnrollmentId,
            request.AttendanceDate,
            request.EventType,
            request.Status,
            cancellationToken
        );
        var response = new AttendanceRecordResponse(
            attendanceRecord.Id,
            attendanceRecord.EnrollmentId,
            attendanceRecord.AttendanceDate,
            attendanceRecord.EventType,
            attendanceRecord.Status
        );
        return CreatedAtAction(nameof(GetById), new { id = attendanceRecord.Id }, response);
    }
    [HttpPatch("{id:guid}")]
    public async Task<ActionResult<AttendanceRecordResponse>> Update(
        Guid id,
        UpdateAttendanceRecordRequest request,
        CancellationToken cancellationToken
    )
    {
        var attendanceRecord = await _attendanceRecordService.UpdateAttendanceRecordAsync(
            id,
            request.Status,
            cancellationToken
        );
        if (attendanceRecord is null)
        {
            return NotFound();
        }
        var response = new AttendanceRecordResponse(
            attendanceRecord.Id,
            attendanceRecord.EnrollmentId,
            attendanceRecord.AttendanceDate,
            attendanceRecord.EventType,
            attendanceRecord.Status
        );
        return Ok(response);
    }
}