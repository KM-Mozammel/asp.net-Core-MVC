namespace StudentPortal.Models.Entities
{
    public class Student
    {

        // We Will Collect those Information by Form
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool Subscribed { get; set; }

    }
}
