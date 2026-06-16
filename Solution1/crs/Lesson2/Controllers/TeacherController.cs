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

        public TeacherController(ITeacherService teacherService)
        {
            TeacherService = teacherService;
        }

        [HttpGet]
        public async Task<IEnumerable<TeacherGetDto>> GetAll()
        {
            return await TeacherService.GetAllTeachersAsync();
        }

        [HttpGet("{id:guid}")]
        public async Task<TeacherGetDto> GetById(Guid id)
        {
            return await TeacherService.GetTeacherByIdAsync(id);
        }

        [HttpPost]
        public async Task Create([FromBody] TeacherCreateDto dto)
        {
            await TeacherService.CreateTeacherAsync(dto);
        }

        [HttpPut("{id:guid}")]
        public async Task Update(Guid id, [FromBody] TeacherCreateDto dto)
        {
            await TeacherService.UpdateTeacherAsync(id, dto);
        }

        [HttpDelete("{id:guid}")]
        public async Task Delete(Guid id)
        {
            await TeacherService.DeleteTeacherAsync(id);
        }

    }
}
