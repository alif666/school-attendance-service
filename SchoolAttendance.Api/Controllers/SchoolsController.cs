using Microsoft.AspNetCore.Mvc;
using SchoolAttendance.Api.Models.Schools;
using SchoolAttendance.Application.Schools;
namespace SchoolAttendance.Api.Controllers;

[ApiController]
[Route("api/schools")]
public class SchoolsController : ControllerBase
{
    private readonly ISchoolService _schoolService;
    public SchoolsController(ISchoolService schoolService)
    {
        _schoolService = schoolService;
    }
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<SchoolResponse>>> GetAll(CancellationToken cancellationToken)
    {
        var schools = await _schoolService.GetAllAsync(cancellationToken);
        var response = schools.Select(school => new SchoolResponse(
            school.Id,
            school.SchoolCode,
            school.SchoolName
        )).ToList();
        return Ok(response);
    }
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<SchoolResponse>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var school = await _schoolService.GetByIdAsync(id, cancellationToken);
        if (school is null)
        {
            return NotFound();
        }
        var response = new SchoolResponse(
            school.Id,
            school.SchoolCode,
            school.SchoolName
        );
        return Ok(response);
    }
    [HttpPost]
    public async Task<ActionResult<SchoolResponse>> Create(CreateSchoolRequest request, CancellationToken cancellationToken)
    {
        var school = await _schoolService.CreateSchoolAsync(
            request.SchoolCode,
            request.SchoolName,
            cancellationToken
        );
        var response = new SchoolResponse(
            school.Id,
            school.SchoolCode,
            school.SchoolName
        );
        return CreatedAtAction(nameof(GetById), new { id = school.Id }, response);
    }
    [HttpPatch("{id:guid}")]
    public async Task<ActionResult<SchoolResponse>> Update(
        Guid id,
        UpdateSchoolRequest request,
        CancellationToken cancellationToken
    )
    {
        var school = await _schoolService.UpdateSchoolAsync(
            id,
            request.SchoolName,
            cancellationToken
        );
        if (school is null)
        {
            return NotFound();
        }
        var response = new SchoolResponse(
            school.Id,
            school.SchoolCode,
            school.SchoolName
        );
        return Ok(response);
    }
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        Guid id,
        CancellationToken cancellationToken
    )
    {
        var isDeactivated = await _schoolService.DeactivateSchoolAsync(id, cancellationToken);
        if (!isDeactivated)
        {
            return NotFound();
        }
        return NoContent();
    }
}