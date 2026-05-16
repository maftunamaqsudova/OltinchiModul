namespace Lesson2.Dtos
{
    public class StudentCreateDto
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Grade { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public List<Guid> TeacherIds { get; set; } = new List<Guid>();
    }
}
