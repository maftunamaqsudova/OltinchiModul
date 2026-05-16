namespace Lesson2.Dtos
{
    public class StudentGetDto
    {
        public Guid StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Grade { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public List<TeacherGetDto> Teachers { get; set; } = new List<TeacherGetDto>();
    }
}
