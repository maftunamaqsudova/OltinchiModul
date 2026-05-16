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

        // Dependency Injection orqali servisni controllerga ulaymiz
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }


        [HttpGet]
        public async Task<IEnumerable<StudentGetDto>> GetAll()
        {
            return await _studentService.GetAllStudentsAsync();
        }

        // 2. GET: api/students/{id} (Faqat bitta talaba obyekti qaytadi)
        [HttpGet("{id:guid}")]
        public async Task<StudentGetDto> GetById(Guid id)
        {
            return await _studentService.GetStudentByIdAsync(id);
        }

        // 3. POST: api/students (Yaratilganda hech narsa qaytarmaydi, shunchaki bajariladi)
        [HttpPost]
        public async Task Create([FromBody] StudentCreateDto dto)
        {
            await _studentService.CreateStudentAsync(dto);
        }

        // 4. PUT: api/students/{id}
        [HttpPut("{id:guid}")]
        public async Task Update(Guid id, [FromBody] StudentCreateDto dto)
        {
            await _studentService.UpdateStudentAsync(id, dto);
        }

        // 5. DELETE: api/students/{id}
        [HttpDelete("{id:guid}")]
        public async Task Delete(Guid id)
        {
            await _studentService.DeleteStudentAsync(id);
        }
    }
}
   
