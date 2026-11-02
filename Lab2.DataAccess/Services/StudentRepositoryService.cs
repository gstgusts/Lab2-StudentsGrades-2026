using Lab2.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lab2.DataAccess.Services
{
    public class StudentRepositoryService : IStudentRepository
    {
        private readonly StudentDbContext _dbContext;

        public StudentRepositoryService(StudentDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbContext.Database.EnsureCreated();
        }

        public IEnumerable<Student> GetAll()
        {
            return _dbContext.Students.Include(s => s.Grades).ToList();
        }

        public Student? GetById(int id)
        {
            return _dbContext.Students.Include(s => s.Grades)
                .FirstOrDefault(s => s.Id == id);
        }

        public int Add(Student student)
        {
            _dbContext.Students.Add(student);
            _dbContext.SaveChanges();
            return student.Id;
        }

        public void Update(Student student)
        {
            _dbContext.Students.Update(student);
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var student = GetById(id);

            if (student == null) return;

            _dbContext.Students.Remove(student);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Grade> GetGradesByStudentId(int studentId)
        {
            var student = GetById(studentId);

            if (student == null) return Enumerable.Empty<Grade>();

            return student.Grades.ToList();
        }
    }
}
