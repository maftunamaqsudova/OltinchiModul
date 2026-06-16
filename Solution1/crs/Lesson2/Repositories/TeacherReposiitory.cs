using System.Linq;
using Lesson2.Data;
using Lesson2.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lesson2.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly AppDbContext _context;
        public TeacherRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Teacher>> GetAllAsync()
        {
            return await _context.Teachers
                .Include(t => t.Students) 
                .ToListAsync();
        }

        public async Task<Teacher>? GetByIdAsync(Guid id)
        {
            return await _context.Teachers
                .Include(t => t.Students)
                .FirstOrDefaultAsync(t => t.TeacherId == id);
        }

        public async Task AddAsync(Teacher teacher, List<Guid> studentIds)
        {
            if (studentIds != null && studentIds.Any())
            {
                var students = await _context.Students
                    .Where(s => studentIds.Contains(s.StudentId))
                    .ToListAsync();

                foreach (var student in students)
                {
                    teacher.Students.Add(student);
                }
            }

            await _context.Teachers.AddAsync(teacher);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Teacher teacher, List<Guid> studentIds)
        {
            var trackedTeacher = await _context.Teachers
                .Include(t => t.Students)
                .FirstOrDefaultAsync(t => t.TeacherId == teacher.TeacherId);

            if (trackedTeacher != null)
            {
                trackedTeacher.FirstName = teacher.FirstName;
                trackedTeacher.LastName = teacher.LastName;
                trackedTeacher.Subject = teacher.Subject;
                trackedTeacher.Email = teacher.Email;
                trackedTeacher.PhoneNumber = teacher.PhoneNumber;

               
                trackedTeacher.Students.Clear();

                
                if (studentIds != null && studentIds.Any())
                {
                    var newStudents = await _context.Students
                        .Where(s => studentIds.Contains(s.StudentId))
                        .ToListAsync();

                    foreach (var student in newStudents)
                    {
                        trackedTeacher.Students.Add(student);
                    }
                }

                _context.Teachers.Update(trackedTeacher);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher != null)
            {
                _context.Teachers.Remove(teacher);
                
                await _context.SaveChangesAsync();
            }
        }
    }
}
