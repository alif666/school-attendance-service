using Microsoft.AspNetCore.Mvc;
using SchoolAttendance.Api.Models.Students;
using SchoolAttendance.Application.Students;

namespace SchoolAttendance.Api.Controllers;

[ApiController]
[Route("api/students")]
public class StudentsController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentsController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<StudentResponse>>> GetAll(CancellationToken cancellationToken)
    {
        var students = await _studentService.GetAllAsync(cancellationToken);
        var response = students.Select(student => new StudentResponse(
            student.Id,
            student.StudentCode,
            student.FullName,
            student.DateOfBirth
        )).ToList();
        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<StudentResponse>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var student = await _studentService.GetByIdAsync(id, cancellationToken);
        if (student is null)
        {
            return NotFound();
        }
        var response = new StudentResponse(
            student.Id,
            student.StudentCode,
            student.FullName,
            student.DateOfBirth
        );
        return Ok(response);
    }


    [HttpPost]
    public async Task<ActionResult<StudentResponse>> Create(CreateStudentRequest request, CancellationToken cancellationToken)
    {
        var student = await _studentService.CreateStudentAsync(
        request.StudentCode,
        request.FullName,
        request.DateOfBirth,
        cancellationToken);
        var response = new StudentResponse(
            student.Id,
            student.StudentCode,
            student.FullName,
            student.DateOfBirth);
        return CreatedAtAction(nameof(GetById), new { id = student.Id }, response);
    }

    [HttpPatch("{id:guid}")]
    public async Task<ActionResult<StudentResponse>> Update(
        Guid id,
        UpdateStudentRequest request,
        CancellationToken cancellationToken
    )
    {
        var student = await _studentService.UpdateStudentAsync(
            id,
            request.FullName,
            request.DateOfBirth,
            cancellationToken
        );
        if (student is null)
        {
            return NotFound();
        }
        var response = new StudentResponse(
            student.Id,
            student.StudentCode,
            student.FullName,
            student.DateOfBirth
        );
        return Ok(response);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        Guid id,
        CancellationToken cancellationToken
    )
    {
        var isDeactivated = await _studentService.DeactivateStudentAsync(id,cancellationToken);
        if (!isDeactivated)
        {
            return NotFound();
        }
        return NoContent();
    }
}