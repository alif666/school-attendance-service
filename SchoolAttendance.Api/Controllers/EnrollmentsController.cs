using Microsoft.AspNetCore.Mvc;
using SchoolAttendance.Api.Models.Enrollments;
using SchoolAttendance.Application.Enrollments;

namespace SchoolAttendance.Api.Controllers;

[ApiController]
[Route("api/enrollments")]
public class EnrollmentsController : ControllerBase
{
    private readonly IEnrollmentService _enrollmentService;
    public EnrollmentsController(IEnrollmentService enrollmentService)
    {
        _enrollmentService = enrollmentService;
    }
    [HttpGet("sections")]
    public async Task<ActionResult<IReadOnlyList<EnrollmentResponse>>> GetBySectionIdAndAcademicSessionId(Guid sectionId, Guid academicSessionId, CancellationToken cancellationToken)
    {
        var enrollments = await _enrollmentService.GetBySectionIdAndAcademicSessionIdAsync(sectionId, academicSessionId, cancellationToken);
        var response = enrollments.Select(
            enrollment => new EnrollmentResponse(
                enrollment.Id,
                enrollment.StudentId,
                enrollment.AcademicSessionId,
                enrollment.SectionId,
                enrollment.EnrollmentDate
            )).ToList();
        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<EnrollmentResponse>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var enrollment = await _enrollmentService.GetByIdAsync(id, cancellationToken);
        if (enrollment is null)
        {
            return NotFound();
        }
        var response = new EnrollmentResponse(
            enrollment.Id,
            enrollment.StudentId,
            enrollment.AcademicSessionId,
            enrollment.SectionId,
            enrollment.EnrollmentDate
        );
        return Ok(response);
    }
    [HttpGet("students")]
    public async Task<ActionResult<EnrollmentResponse>> GetByStudentIdAndAcademicSessionId(Guid studentId, Guid academicSessionId, CancellationToken cancellationToken)
    {
        var enrollment = await _enrollmentService.GetByStudentIdAndAcademicSessionIdAsync(studentId, academicSessionId, cancellationToken);
        if (enrollment is null)
        {
            return NotFound();
        }
        var response = new EnrollmentResponse(
            enrollment.Id,
            enrollment.StudentId,
            enrollment.AcademicSessionId,
            enrollment.SectionId,
            enrollment.EnrollmentDate
        );
        return Ok(response);
    }
    [HttpPost]
    public async Task<ActionResult<EnrollmentResponse>> Create(CreateEnrollmentRequest request, CancellationToken cancellationToken)
    {
        var enrollment = await _enrollmentService.CreateEnrollmentAsync(
            request.StudentId,
            request.AcademicSessionId,
            request.SectionId,
            request.EnrollmentDate,
            cancellationToken
        );
        var response = new EnrollmentResponse(
            enrollment.Id,
            enrollment.StudentId,
            enrollment.AcademicSessionId,
            enrollment.SectionId,
            enrollment.EnrollmentDate
        );
        return CreatedAtAction(nameof(GetById), new { id = enrollment.Id }, response);
    }
    [HttpPatch("{id:guid}")]
    public async Task<ActionResult<EnrollmentResponse>> Update(
        Guid id,
        UpdateEnrollmentRequest request,
        CancellationToken cancellationToken
    )
    {
        var enrollment = await _enrollmentService.UpdateEnrollmentAsync(
            id,
            request.SectionId,
            cancellationToken
        );
        if (enrollment is null)
        {
            return NotFound();
        }
        var response = new EnrollmentResponse(
            enrollment.Id,
            enrollment.StudentId,
            enrollment.AcademicSessionId,
            enrollment.SectionId,
            enrollment.EnrollmentDate
        );
        return Ok(response);
    }
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        Guid id,
        CancellationToken cancellationToken
    )
    {
        var isDeactivated = await _enrollmentService.DeactivateEnrollmentAsync(id, cancellationToken);
        if (!isDeactivated)
        {
            return NotFound();
        }
        return NoContent();
    }
}