using Microsoft.AspNetCore.Mvc;
using SchoolAttendance.Api.Models.Schools;
using SchoolAttendance.Application.Schools;

namespace SchoolAttendance.Api.Controllers;
[ApiController]
[Route("api/branches")]
public class BranchesController : ControllerBase
{
    private readonly IBranchService _branchService;
    public BranchesController(IBranchService branchService)
    {
        _branchService = branchService;
    }
    [HttpGet("school/{schoolId:Guid}")]
    public async Task<ActionResult<IReadOnlyList<BranchResponse>>> GetAllBySchoolId(Guid schoolId, CancellationToken cancellationToken)
    {
        var branches = await _branchService.GetBySchoolIdAsync(schoolId, cancellationToken);
        var response = branches.Select(branch => new BranchResponse(
            branch.Id,
            branch.SchoolId,
            branch.BranchCode,
            branch.BranchName,
            branch.Address
        )).ToList();
        return Ok(response);
    }
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<BranchResponse>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var branch = await _branchService.GetByIdAsync(id, cancellationToken);
        if (branch is null)
        {
            return NotFound();
        }
        var response = new BranchResponse(
            branch.Id,
            branch.SchoolId,
            branch.BranchCode,
            branch.BranchName,
            branch.Address
        );
        return Ok(response);
    }
    [HttpPost]
    public async Task<ActionResult<BranchResponse>> Create(CreateBranchRequest request, CancellationToken cancellationToken)
    {
        var branch = await _branchService.CreateBranchAsync(
            request.SchoolId,
            request.BranchCode,
            request.BranchName,
            request.Address,
            cancellationToken
        );
        var response = new BranchResponse(
            branch.Id,
            branch.SchoolId,
            branch.BranchCode,
            branch.BranchName,
            branch.Address
        );
        return CreatedAtAction(nameof(GetById), new { id = branch.Id }, response);
    }
    [HttpPatch("{id:guid}")]
    public async Task<ActionResult<BranchResponse>> Update(
        Guid id,
        UpdateBranchRequest request,
        CancellationToken cancellationToken
    )
    {
        var branch = await _branchService.UpdateBranchAsync(
            id,
            request.BranchName,
            request.Address,
            cancellationToken
        );
        if (branch is null)
        {
            return NotFound();
        }
        var response = new BranchResponse(
            branch.Id,
            branch.SchoolId,
            branch.BranchCode,
            branch.BranchName,
            branch.Address
        );
        return Ok(response);
    }
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var isDeactivated = await _branchService.DeactivateBranchAsync(
            id,
            cancellationToken
        );
        if (!isDeactivated)
        {
            return NotFound();
        }
        return NoContent();
    }
}