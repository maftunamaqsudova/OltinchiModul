using Lesson2.Dtos;

namespace Lesson2.Services
{
    public interface ITeacherService
    {
        Task<IEnumerable<TeacherGetDto>> GetAllTeachersAsync();
        Task<TeacherGetDto> GetTeacherByIdAsync(Guid id);
        Task CreateTeacherAsync(TeacherCreateDto dto);
        Task UpdateTeacherAsync(Guid id, TeacherCreateDto dto);
        Task DeleteTeacherAsync(Guid id);
    }
}