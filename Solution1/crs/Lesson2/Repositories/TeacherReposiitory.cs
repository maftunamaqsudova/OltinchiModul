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

        // 1. Barcha o'qituvchilarni talabalari bilan birga olish
        public async Task<IEnumerable<Teacher>> GetAllAsync()
        {
            return await _context.Teachers
                .Include(t => t.Students) // Ko'pga-ko'p bog'langan studentlarni yuklash
                .ToListAsync();
        }

        // 2. Bitta o'qituvchini ID bo'yicha talabalari bilan birga olish
        public async Task<Teacher>? GetByIdAsync(Guid id)
        {
            return await _context.Teachers
                .Include(t => t.Students)
                .FirstOrDefaultAsync(t => t.TeacherId == id);
        }

        // 3. Yangi o'qituvchi yaratish va unga talabalarni biriktirish
        public async Task AddAsync(Teacher teacher, List<Guid> studentIds)
        {
            if (studentIds != null && studentIds.Any())
            {
                // Bazadan kelgan IDlar bo'yicha studentlarni topamiz
                var students = await _context.Students
                    .Where(s => studentIds.Contains(s.StudentId))
                    .ToListAsync();

                // O'qituvchining Students kolleksiyasiga qo'shamiz
                foreach (var student in students)
                {
                    teacher.Students.Add(student);
                }
            }

            await _context.Teachers.AddAsync(teacher);
            await _context.SaveChangesAsync();
        }

        // 4. O'qituvchi ma'lumotlarini va uning talabalari ro'yxatini yangilash
        public async Task UpdateAsync(Teacher teacher, List<Guid> studentIds)
        {
            // Bazadagi eski o'qituvchini va uning hozirgi studentlarini yuklaymiz
            var trackedTeacher = await _context.Teachers
                .Include(t => t.Students)
                .FirstOrDefaultAsync(t => t.TeacherId == teacher.TeacherId);

            if (trackedTeacher != null)
            {
                // Oddiy maydonlarni yangilaymiz
                trackedTeacher.FirstName = teacher.FirstName;
                trackedTeacher.LastName = teacher.LastName;
                trackedTeacher.Subject = teacher.Subject;
                trackedTeacher.Email = teacher.Email;
                trackedTeacher.PhoneNumber = teacher.PhoneNumber;
                // Agar boshqa propertylar bo'lsa, ularni ham: trackedTeacher.Subject = teacher.Subject;

                // KO'PGA-KO'P YANGILASH MANTIQI:
                // 1. Eski bog'lanishlarni tozalaymiz
                trackedTeacher.Students.Clear();

                // 2. Yangi yuborilgan studentlarni bazadan topib qayta biriktiramiz
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

        // 5. O'qituvchini o'chirish
        public async Task DeleteAsync(Guid id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher != null)
            {
                _context.Teachers.Remove(teacher);
                // Configuration'da Cascade Delete qo'yganimiz uchun, 
                // Teacher_Student jadvalidagi daxldor qatorlar avtomatik o'chadi.
                await _context.SaveChangesAsync();
            }
        }
    }
}
