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
                .Include(s => s.Teachers) // Talabaga biriktirilgan ustozlarni yuklash
                .ToListAsync();
        }

        // 2. Bitta talabani ID bo'yicha ustozlari bilan birga olish
        public async Task<Student> GetByIdAsync(Guid id)
        {
            return await _context.Students
                .Include(s => s.Teachers)
                .FirstOrDefaultAsync(s => s.StudentId == id);
        }

        // 3. Yangi talaba yaratish va unga o'qituvchilarni biriktirish
        public async Task AddAsync(Student student, List<Guid> teacherIds)
        {
            if (teacherIds != null && teacherIds.Any())
            {
                // Kelgan IDlar bo'yicha o'qituvchilarni bazadan topamiz
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

        // 4. Talaba ma'lumotlarini va uning ustozlari ro'yxatini yangilash
        public async Task UpdateAsync(Student student, List<Guid> teacherIds)
        {
            // Bazadagi eski talaba va uning ustozlarini yuklaymiz
            var trackedStudent = await _context.Students
                .Include(s => s.Teachers)
                .FirstOrDefaultAsync(s => s.StudentId == student.StudentId);

            if (trackedStudent != null)
            {
                // Oddiy maydonlarni yangilash
                trackedStudent.FirstName = student.FirstName;
                trackedStudent.LastName = student.LastName;
                trackedStudent.Email = student.Email;
                trackedStudent.PhoneNumber = student.PhoneNumber;
                trackedStudent.Grade = student.Grade;
                // Agar boshqa propertylar bo'lsa (masalan, Email):
                // trackedStudent.Email = student.Email;

                // KO'PGA-KO'P YANGILASH MANTIQI:
                // 1. Eski bog'lanishlarni (ustozlarni) o'chiramiz
                trackedStudent.Teachers.Clear();

                // 2. Yangi yuborilgan IDlar bo'yicha ustozlarni topib qayta biriktiramiz
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

        // 5. Talabani o'chirish
        public async Task DeleteAsync(Guid id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                // TeacherConfiguration'da Cascade Delete qo'yganimiz sababli,
                // oraliq Teacher_Student jadvalidagi daxldor qatorlar ham avtomatik o'chadi.
                await _context.SaveChangesAsync();
            }
        }
    }
}
