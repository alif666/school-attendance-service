using SchoolAttendance.Domain.Students;

namespace SchoolAttendance.Application.Students;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;

    public StudentService(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<Student> CreateStudentAsync(string studentCode, string fullName, DateOnly? dateOfBirth = null, CancellationToken cancellationToken = default)
    {
        var student = new Student(
            studentCode,
            fullName,
            dateOfBirth
        );
        await _studentRepository.AddAsync(student, cancellationToken);

        return student;
    }



    public async Task<IReadOnlyList<Student>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _studentRepository.GetAllAsync(cancellationToken);
    }

    public async Task<Student?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _studentRepository.GetByIdAsync(id, cancellationToken);
    }

    public async Task<Student?> UpdateStudentAsync(Guid id, string fullName, DateOnly? dateOfBirth = null, CancellationToken cancellationToken = default)
    {
        var student = await _studentRepository.GetByIdAsync(id, cancellationToken);
        if (student is null)
        {
            return null;
        }
        student.UpdateProfile(fullName, dateOfBirth);
        await _studentRepository.UpdateAsync(student, cancellationToken);
        return student;
    }

    public async Task<bool> DeactivateStudentAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var student = await _studentRepository.GetByIdAsync(id, cancellationToken);
        if (student is null)
        {
            return false;
        }
        student.Deactivate();
        await _studentRepository.UpdateAsync(student, cancellationToken);
        return true;
    }
}