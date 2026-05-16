using Lesson2.Dtos;
using Lesson2.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lesson2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService TeacherService;

        // Dependency Injection orqali servisni controllerga ulaymiz
        public TeacherController(ITeacherService teacherService)
        {
            TeacherService = teacherService;
        }

        [HttpGet]
        public async Task<IEnumerable<TeacherGetDto>> GetAll()
        {
            return await TeacherService.GetAllTeachersAsync();
        }

        // 2. GET: api/teachers/{id} (Bitta o'qituvchini ID bo'yicha olish)
        [HttpGet("{id:guid}")]
        public async Task<TeacherGetDto> GetById(Guid id)
        {
            return await TeacherService.GetTeacherByIdAsync(id);
        }

        // 3. POST: api/teachers (Yangi o'qituvchi yaratish)
        [HttpPost]
        public async Task Create([FromBody] TeacherCreateDto dto)
        {
            await TeacherService.CreateTeacherAsync(dto);
        }

        // 4. PUT: api/teachers/{id} (O'qituvchi ma'lumotlarini yangilash)
        [HttpPut("{id:guid}")]
        public async Task Update(Guid id, [FromBody] TeacherCreateDto dto)
        {
            await TeacherService.UpdateTeacherAsync(id, dto);
        }

        // 5. DELETE: api/teachers/{id} (O'qituvchini o'chirish)
        [HttpDelete("{id:guid}")]
        public async Task Delete(Guid id)
        {
            await TeacherService.DeleteTeacherAsync(id);
        }

    }
}
