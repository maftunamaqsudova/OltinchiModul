using Lesson2.Dtos;
using Lesson2.Entities;
using Lesson2.Repositories;

namespace Lesson2.Services;
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<IEnumerable<StudentGetDto>> GetAllStudentsAsync()
        {
            var students = await _studentRepository.GetAllAsync();

            var studentDtos = students.Select(s => new StudentGetDto
            {
                StudentId = s.StudentId,
                FirstName = s.FirstName,
                Email = s.Email,
                PhoneNumber = s.PhoneNumber,
                Grade = s.Grade,
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

        public async Task CreateStudentAsync(StudentCreateDto dto)
        {
            var student = new Student
            {
                FirstName = dto.FirstName,
                Email = dto.Email,
                LastName = dto.LastName,
                PhoneNumber= dto.PhoneNumber,
                Grade = dto.Grade,

            };

            await _studentRepository.AddAsync(student, dto.TeacherIds);
        }

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

            await _studentRepository.UpdateAsync(student, dto.TeacherIds);
        }

        public async Task DeleteStudentAsync(Guid id)
        {
            await _studentRepository.DeleteAsync(id);
        }
    }

