using Microsoft.AspNetCore.Mvc;
using SchoolAttendance.Api.Models.Sections;
using SchoolAttendance.Application.Sections;

namespace SchoolAttendance.Api.Controllers;

[ApiController]
[Route("/api/sections")]
public class SectionsController : ControllerBase
{
    private readonly ISectionService _sectionService;
    public SectionsController(ISectionService sectionService)
    {
        _sectionService = sectionService;
    }
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<SectionResponse>>> GetBySchoolClassIdAsync(Guid schoolClassId, CancellationToken cancellationToken)
    {
        var sections = await _sectionService.GetBySchoolClassIdAsync(schoolClassId, cancellationToken);
        var response = sections.Select(
            section => new SectionResponse(
                section.Id,
                section.SchoolClassId,
                section.Name
            )).ToList();
        return Ok(response);
    }
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<SectionResponse>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var section = await _sectionService.GetByIdAsync(id, cancellationToken);
        if (section is null)
        {
            return NotFound();
        }
        var response = new SectionResponse(
                section.Id,
                section.SchoolClassId,
                section.Name
            );
        return Ok(response);
    }
    [HttpPost]
    public async Task<ActionResult<SectionResponse>> Create(CreateSectionRequest request, CancellationToken cancellationToken)
    {
        var section = await _sectionService.CreateSectionAsync(
            request.SchoolClassId,
            request.Name,
            cancellationToken
        );
        var response = new SectionResponse(
            section.Id,
            section.SchoolClassId,
            section.Name
        );
        return CreatedAtAction(nameof(GetById), new { id = section.Id }, response);
    }
    [HttpPatch("{id:guid}")]
    public async Task<ActionResult<SectionResponse>> Update(
        Guid id,
        UpdateSectionRequest request,
        CancellationToken cancellationToken
    )
    {
        var section = await _sectionService.UpdateSectionAsync(
            id,
            request.Name,
            cancellationToken
        );
        if (section is null)
        {
            return NotFound();
        }
        var response = new SectionResponse(
            section.Id,
            section.SchoolClassId,
            section.Name
        );
        return Ok(response);
    }
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        Guid id,
        CancellationToken cancellationToken
    )
    {
        var isDeactivated = await _sectionService.DeactivateSectionAsync(id, cancellationToken);
        if (!isDeactivated)
        {
            return NotFound();
        }
        return NoContent();
    }
}