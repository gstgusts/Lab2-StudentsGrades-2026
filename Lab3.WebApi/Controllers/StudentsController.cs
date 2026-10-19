using Lab2.DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab3.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private StudentDbContext dbContext;

        public StudentsController()
        {
            dbContext = new StudentDbContext();
        }

        [HttpGet]
        public IEnumerable<Student> GetStudents()
        {
            return dbContext.Students.Include(s => s.Grades).ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Student> GetStudent(int id)
        {
            var student = dbContext.Students
                .Include(s => s.Grades)
                .FirstOrDefault(s => s.Id == id);

            if (student == null)
            {
                return NotFound();
            }
            return student;
        }

        [HttpPost]
        public ActionResult<Student> CreateStudent(Student student)
        {
            dbContext.Students.Add(student);
            dbContext.SaveChanges();
            return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);
        }

        [HttpPut("{id}")]
        public ActionResult<Student> UpdateStudent(int id, Student student)
        {
            if (id != student.Id)
            {
                return BadRequest();
            }

            dbContext.Students.Update(student);
            dbContext.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<Student> DeleteStudent(int id)
        {
            var student = dbContext.Students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            dbContext.Students.Remove(student);
            dbContext.SaveChanges();
            return NoContent();
        }

    }
}
