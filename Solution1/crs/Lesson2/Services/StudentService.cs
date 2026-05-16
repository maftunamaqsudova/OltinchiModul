using Lesson2.Dtos;
using Lesson2.Entities;
using Lesson2.Repositories;

namespace Lesson2.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        // 1. Hamma talabalarni DTO formatida qaytarish
        public async Task<IEnumerable<StudentGetDto>> GetAllStudentsAsync()
        {
            var students = await _studentRepository.GetAllAsync();

            // Entity ro'yxatini DTO ro'yxatiga o'giramiz (Manual Mapping)
            var studentDtos = students.Select(s => new StudentGetDto
            {
                StudentId = s.StudentId,
                FirstName = s.FirstName,
                Email = s.Email,
                PhoneNumber = s.PhoneNumber,
                Grade = s.Grade,
                // Ko'pga-ko'p bog'langan ustozlarni qisqa DTO ko'rinishida yig'amiz
                Teachers = s.Teachers.Select(t => new TeacherGetDto
                {
                    TeacherId = t.TeacherId,
                    FirstName = t.FirstName,
                    Email = t.Email,
                    PhoneNumber = t.PhoneNumber,
                    LastName = t.LastName,
                    Subject = t.Subject,
                }).ToList()
            });

            return studentDtos;
        }

        // 2. Bitta talabani ID bo'yicha DTO formatida qaytarish
        public async Task<StudentGetDto> GetStudentByIdAsync(Guid id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            if (student == null) return null;

            return new StudentGetDto
            {
                StudentId = student.StudentId,
                FirstName = student.FirstName,
                Email = student.Email,
                Teachers = student.Teachers.Select(t => new TeacherGetDto
                {
                    TeacherId = t.TeacherId,
                    FirstName = t.FirstName,
                    Email = t.Email,
                    PhoneNumber = t.PhoneNumber,
                    LastName= t.LastName,
                    Subject = t.Subject,
                }).ToList()
            };
        }

        // 3. Yangi talaba yaratish (Create)
        public async Task CreateStudentAsync(StudentCreateDto dto)
        {
            // DTOni Entityga o'giramiz
            var student = new Student
            {
                FirstName = dto.FirstName,
                Email = dto.Email,
                LastName = dto.LastName,
                PhoneNumber= dto.PhoneNumber,
                Grade = dto.Grade,

            };

            // Repositoryga yangi studentni va unga biriktirilgan TeacherId'lar ro'yxatini berib yuboramiz
            await _studentRepository.AddAsync(student, dto.TeacherIds);
        }

        // 4. Talaba ma'lumotlarini yangilash (Update)
        public async Task UpdateStudentAsync(Guid id, StudentCreateDto dto)
        {
            var student = new Student
            {
                StudentId = id,
                FirstName = dto.FirstName,
                Email = dto.Email,
                LastName = dto.LastName,
                PhoneNumber = dto.PhoneNumber,
                Grade = dto.Grade,
            };

            // Repository o'zi ichkarida eski bog'lanishlarni tozalab, yangi TeacherId'larni bog'lab qo'yadi
            await _studentRepository.UpdateAsync(student, dto.TeacherIds);
        }

        // 5. Talabani o'chirish (Delete)
        public async Task DeleteStudentAsync(Guid id)
        {
            await _studentRepository.DeleteAsync(id);
        }
    }
}
