using Lesson2.Entities;

namespace Lesson2.Repositories
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetAllAsync();
        Task<Student> GetByIdAsync(Guid id);
        Task AddAsync(Student student, List<Guid> teacherIds);
        Task UpdateAsync(Student student, List<Guid> teacherIds);
        Task DeleteAsync(Guid id);
    }
}