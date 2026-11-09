namespace Lab2.DataAccess.Interfaces
{
    public interface IStudentRepository
    {
        IEnumerable<Student> GetAll();
        IEnumerable<Student> Search(string? code, string? name, string? surname);
        Student? GetById(int id);
        int Add(Student student);
        void Update(Student student);
        void Delete(int id);
        IEnumerable<Grade> GetGradesByStudentId(int studentId);
    }
}
