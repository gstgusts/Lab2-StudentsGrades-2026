namespace Lab4.WpfStudents.Models
{
    public class Grade
    {
        public int Id { get; set; }

        public int? Value { get; set; }

        public string? Comment { get; set; }

        public Subject Subject { get; set; }
    }
}
