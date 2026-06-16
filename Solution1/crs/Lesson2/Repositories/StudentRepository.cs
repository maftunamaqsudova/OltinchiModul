using Lesson2.Data;
using Lesson2.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lesson2.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _context;

        public StudentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _context.Students
                .Include(s => s.Teachers) 
                .ToListAsync();
        }

        
        public async Task<Student> GetByIdAsync(Guid id)
        {
            return await _context.Students
                .Include(s => s.Teachers)
                .FirstOrDefaultAsync(s => s.StudentId == id);
        }

        
        public async Task AddAsync(Student student, List<Guid> teacherIds)
        {
            if (teacherIds != null && teacherIds.Any())
            {
                
                var teachers = await _context.Teachers
                    .Where(t => teacherIds.Contains(t.TeacherId))
                    .ToListAsync();

                // Talabaning Teachers kolleksiyasiga qo'shamiz
                foreach (var teacher in teachers)
                {
                    student.Teachers.Add(teacher);
                }
            }

            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
        }

       
        public async Task UpdateAsync(Student student, List<Guid> teacherIds)
        {
            // Bazadagi eski talaba va uning ustozlarini yuklaymiz
            var trackedStudent = await _context.Students
                .Include(s => s.Teachers)
                .FirstOrDefaultAsync(s => s.StudentId == student.StudentId);

            if (trackedStudent != null)
            {
                
                trackedStudent.FirstName = student.FirstName;
                trackedStudent.LastName = student.LastName;
                trackedStudent.Email = student.Email;
                trackedStudent.PhoneNumber = student.PhoneNumber;
                trackedStudent.Grade = student.Grade;
                
                trackedStudent.Teachers.Clear();

               
                if (teacherIds != null && teacherIds.Any())
                {
                    var newTeachers = await _context.Teachers
                        .Where(t => teacherIds.Contains(t.TeacherId))
                        .ToListAsync();

                    foreach (var teacher in newTeachers)
                    {
                        trackedStudent.Teachers.Add(teacher);
                    }
                }

                _context.Students.Update(trackedStudent);
                await _context.SaveChangesAsync();
            }
        }

        
        public async Task DeleteAsync(Guid id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }
        }
    }
}
