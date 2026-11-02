using Lab2.DataAccess;
using Lab2.DataAccess.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab4.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentRepository _repo;

        public StudentsController(IStudentRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IEnumerable<Student> GetStudents()
        {
            return _repo.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<Student> GetStudent(int id)
        {
            var student = _repo.GetById(id);

            if (student == null)
            {
                return NotFound();
            }
            return student;
        }

        [HttpPost]
        public ActionResult<Student> CreateStudent(Student student)
        {
            _repo.Add(student);
            return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);
        }

        [HttpPut("{id}")]
        public ActionResult<Student> UpdateStudent(int id, Student student)
        {
            if (id != student.Id)
            {
                return BadRequest();
            }

            _repo.Update(student);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<Student> DeleteStudent(int id)
        {
            var student = _repo.GetById(id);
            if (student == null)
            {
                return NotFound();
            }

            _repo.Delete(id);
            return NoContent();
        }

        [HttpGet]
        [Route("{id}/Grades")]
        public IActionResult GetGrades(int id)
        {
            try
            {
                var grades = _repo.GetGradesByStudentId(id);
                return Ok(grades);
            }
            catch (ArgumentException ae)
            {
                return NotFound();
            }
        }
    }
}
