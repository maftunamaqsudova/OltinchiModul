using Lesson2.Dtos;

namespace Lesson2.Services
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentGetDto>> GetAllStudentsAsync();
        Task<StudentGetDto> GetStudentByIdAsync(Guid id);
        Task CreateStudentAsync(StudentCreateDto dto);
        Task UpdateStudentAsync(Guid id, StudentCreateDto dto);
        Task DeleteStudentAsync(Guid id);
    }
}