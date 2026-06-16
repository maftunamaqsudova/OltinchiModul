using Lesson2.Dtos;
using Lesson2.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lesson2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }


        [HttpGet]
        public async Task<IEnumerable<StudentGetDto>> GetAll()
        {
            return await _studentService.GetAllStudentsAsync();
        }

        [HttpGet("{id:guid}")]
        public async Task<StudentGetDto> GetById(Guid id)
        {
            return await _studentService.GetStudentByIdAsync(id);
        }

        [HttpPost]
        public async Task Create([FromBody] StudentCreateDto dto)
        {
            await _studentService.CreateStudentAsync(dto);
        }

        [HttpPut("{id:guid}")]
        public async Task Update(Guid id, [FromBody] StudentCreateDto dto)
        {
            await _studentService.UpdateStudentAsync(id, dto);
        }

        [HttpDelete("{id:guid}")]
        public async Task Delete(Guid id)
        {
            await _studentService.DeleteStudentAsync(id);
        }
    }
}
   
