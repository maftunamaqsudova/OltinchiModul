using Lesson2.Entities;

namespace Lesson2.Repositories
{
    public interface ITeacherRepository
    {
        Task<IEnumerable<Teacher>> GetAllAsync();
        Task<Teacher>? GetByIdAsync(Guid id);
        Task AddAsync(Teacher teacher, List<Guid> studentIds);
        Task UpdateAsync(Teacher teacher, List<Guid> studentIds);
        Task DeleteAsync(Guid id);
    }
}