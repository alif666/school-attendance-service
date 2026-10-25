using Microsoft.AspNetCore.Mvc;
using SchoolAttendance.Api.Models.AcademicSessions;
using SchoolAttendance.Application.AcademicSessions;

namespace SchoolAttendance.Api.Controllers;

[ApiController]
[Route("/api/academic-sessions")]
public class AcademicSessionsController : ControllerBase
{
    private readonly IAcademicSessionService _academicSessionService;
    public AcademicSessionsController(IAcademicSessionService academicSessionService)
    {
        _academicSessionService = academicSessionService;
    }
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<AcademicSessionResponse>>> GetByBranchIdAsync(Guid branchId, CancellationToken cancellationToken)
    {
        var academicSessions = await _academicSessionService.GetByBranchIdAsync(branchId, cancellationToken);
        var response = academicSessions.Select(
            academicSession => new AcademicSessionResponse(
                academicSession.Id,
                academicSession.BranchId,
                academicSession.Name,
                academicSession.StartDate,
                academicSession.EndDate
            )).ToList();
        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<AcademicSessionResponse>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var academicSession = await _academicSessionService.GetByIdAsync(id, cancellationToken);
        if (academicSession is null)
        {
            return NotFound();
        }
        var response = new AcademicSessionResponse(
            academicSession.Id,
            academicSession.BranchId,
            academicSession.Name,
            academicSession.StartDate,
            academicSession.EndDate
        );
        return Ok(response);
    }
    [HttpPost]
    public async Task<ActionResult<AcademicSessionResponse>> Create(CreateAcademicSessionRequest request, CancellationToken cancellationToken)
    {
        var academicSession = await _academicSessionService.CreateAcademicSessionAsync(
            request.BranchId,
            request.Name,
            request.StartDate,
            request.EndDate,
            cancellationToken
        );
        var response = new AcademicSessionResponse(
            academicSession.Id,
            academicSession.BranchId,
            academicSession.Name,
            academicSession.StartDate,
            academicSession.EndDate
        );
        return CreatedAtAction(nameof(GetById), new { id = academicSession.Id }, response);
    }
    [HttpPatch("{id:guid}")]
    public async Task<ActionResult<AcademicSessionResponse>> Update(
        Guid id,
        UpdateAcademicSessionRequest request,
        CancellationToken cancellationToken
    )
    {
        var academicSession = await _academicSessionService.UpdateAcademicSessionAsync(
            id,
            request.Name,
            request.StartDate,
            request.EndDate,
            cancellationToken
        );
        if (academicSession is null)
        {
            return NotFound();
        }
        var response = new AcademicSessionResponse(
            academicSession.Id,
            academicSession.BranchId,
            academicSession.Name,
            academicSession.StartDate,
            academicSession.EndDate
        );
        return Ok(response);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        Guid id,
        CancellationToken cancellationToken
    )
    {
        var isDeactivated = await _academicSessionService.DeactivateAcademicSessionAsync(id, cancellationToken);
        if (!isDeactivated)
        {
            return NotFound();
        }
        return NoContent();
    }
}