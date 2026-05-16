namespace Lesson2.Dtos
{
    public class TeacherGetDto
    {
        public Guid TeacherId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Subject { get; set; }
        public List<StudentGetDto> Students { get; set; } = new List<StudentGetDto>();
    }
}
