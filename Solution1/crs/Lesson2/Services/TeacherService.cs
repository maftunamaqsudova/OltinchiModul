using Lesson2.Dtos;
using Lesson2.Entities;
using Lesson2.Repositories;

namespace Lesson2.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _teacherRepository;

        public TeacherService(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        public async Task<IEnumerable<TeacherGetDto>> GetAllTeachersAsync()
        {
            var teachers = await _teacherRepository.GetAllAsync();

            return teachers.Select(t => new TeacherGetDto
            {
                TeacherId = t.TeacherId,
                FirstName = t.FirstName,
                LastName = t.LastName,
                Email = t.Email,
                PhoneNumber = t.PhoneNumber,
                Subject = t.Subject,
                Students = t.Students.Select(s => new StudentGetDto
                {
                    StudentId = s.StudentId,
                    FirstName = s.FirstName,
                    Email = s.Email,
                    PhoneNumber = s.PhoneNumber,
                    Grade = s.Grade
                }).ToList()
            }).ToList();
        }

        public async Task<TeacherGetDto> GetTeacherByIdAsync(Guid id)
        {
            var teacher = await _teacherRepository.GetByIdAsync(id);
            if (teacher == null) return null;

            return new TeacherGetDto
            {
                TeacherId = teacher.TeacherId,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                Email = teacher.Email,
                PhoneNumber = teacher.PhoneNumber,
                Subject = teacher.Subject,
                Students = teacher.Students.Select(s => new StudentGetDto
                {
                    StudentId = s.StudentId,
                    FirstName = s.FirstName,
                    Email = s.Email,
                    PhoneNumber = s.PhoneNumber,
                    Grade = s.Grade
                }).ToList()
            };
        }

        
        public async Task CreateTeacherAsync(TeacherCreateDto dto)
        {
            var teacher = new Teacher
            {
                TeacherId = Guid.NewGuid(), 
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Subject = dto.Subject
            };

            await _teacherRepository.AddAsync(teacher, dto.StudentIds);
        }

        public async Task UpdateTeacherAsync(Guid id, TeacherCreateDto dto)
        {
            var teacher = new Teacher
            {
                TeacherId = id,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Subject = dto.Subject
            };

            await _teacherRepository.UpdateAsync(teacher, dto.StudentIds);
        }

        public async Task DeleteTeacherAsync(Guid id)
        {
            await _teacherRepository.DeleteAsync(id);
        }
    }
}
