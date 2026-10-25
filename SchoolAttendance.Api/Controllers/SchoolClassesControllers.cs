using Microsoft.AspNetCore.Mvc;
using SchoolAttendance.Api.Models.SchoolClasses;
using SchoolAttendance.Application.SchoolClasses;

namespace SchoolAttendance.Api.Controllers;

[ApiController]
[Route("/api/school-classes")]
public class SchoolClassesController : ControllerBase
{
    private readonly ISchoolClassService _schoolClassService;
    public SchoolClassesController(ISchoolClassService schoolClassService)
    {
        _schoolClassService = schoolClassService;
    }
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<SchoolClassResponse>>> GetByBranchIdAsync(Guid branchId, CancellationToken cancellationToken)
    {
        var schoolClasses = await _schoolClassService.GetByBranchIdAsync(branchId, cancellationToken);
        var response = schoolClasses.Select(
            schoolClass => new SchoolClassResponse(
                schoolClass.Id,
                schoolClass.BranchId,
                schoolClass.Name
            )).ToList();
        return Ok(response);
    }
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<SchoolClassResponse>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var schoolClass = await _schoolClassService.GetByIdAsync(id, cancellationToken);
        if (schoolClass is null)
        {
            return NotFound();
        }
        var response = new SchoolClassResponse(
                schoolClass.Id,
                schoolClass.BranchId,
                schoolClass.Name
            );
        return Ok(response);
    }
    [HttpPost]
    public async Task<ActionResult<SchoolClassResponse>> Create(CreateSchoolClassRequest request, CancellationToken cancellationToken)
    {
        var schoolClass = await _schoolClassService.CreateSchoolClassAsync(
            request.BranchId,
            request.Name,
            cancellationToken
        );
        var response = new SchoolClassResponse(
            schoolClass.Id,
            schoolClass.BranchId,
            schoolClass.Name
        );
        return CreatedAtAction(nameof(GetById), new { id = schoolClass.Id }, response);
    }
    [HttpPatch("{id:guid}")]
    public async Task<ActionResult<SchoolClassResponse>> Update(
        Guid id,
        UpdateSchoolClassRequest request,
        CancellationToken cancellationToken
    )
    {
        var schoolClass = await _schoolClassService.UpdateSchoolClassAsync(
            id,
            request.Name,
            cancellationToken
        );
        if (schoolClass is null)
        {
            return NotFound();
        }
        var response = new SchoolClassResponse(
            schoolClass.Id,
            schoolClass.BranchId,
            schoolClass.Name
        );
        return Ok(response);
    }
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        Guid id,
        CancellationToken cancellationToken
    )
    {
        var isDeactivated = await _schoolClassService.DeactivateSchoolClassAsync(id, cancellationToken);
        if (!isDeactivated)
        {
            return NotFound();
        }
        return NoContent();
    }
}