namespace Lesson2.Entities
{
    public class Teacher
    {
        public Guid TeacherId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Subject {  get; set; }

        public virtual ICollection<Student> Students { get; set; } = new List<Student>();
    }

}

