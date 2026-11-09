namespace Lab4.WpfStudents.Models
{
    public class Student
    {
        public int Id { get; set; }

        public string Name { get; set; } = "";

        public string Surname { get; set; } = "";

        public string Code { get; set; } = "";

        public List<Grade> Grades { get; } = new();

        public string FullName => $"{Name} {Surname}";
    }
}
