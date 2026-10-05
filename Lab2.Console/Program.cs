using Lab2.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Lab2.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var db = new StudentDbContext();

            //LINQ
            var student = db.Students
                .Include(s=>s.Grades)
                .ThenInclude(a => a.Subject)
                .FirstOrDefault(s => s.Name.StartsWith("A"));

            if (student != null)
            {
                WriteLine(student.Name);

                foreach (var studentGrade in student.Grades)
                {
                    WriteLine(studentGrade.Subject.Name +" "+studentGrade.Value);
                }
            }

            var students = db.Students.Where(s => s.Surname.Contains("s"));

            foreach (var stud in students)
            {
                WriteLine(stud.Name +" "+ stud.Surname);
            }

            // ADD record
            //var stud2 = new Student
            //{
            //    Name = "Dace",
            //    Surname = "Liepa",
            //    Code = "S0002"
            //};

            //db.Add(stud2);
            //db.SaveChanges();

            // UPDATE record

            //var dace = db.Students.FirstOrDefault(s => s.Code == "S0002");

            //if (dace != null)
            //{
            //    dace.Surname = "Ozols";
            //    db.SaveChanges();
            //}


            //var dace = db.Students.FirstOrDefault(s => s.Code == "S0002");

            //if (dace != null)
            //{
            //    db.Remove(dace);
            //    db.SaveChanges();
            //}
        }

        static void WriteLine(string s)
        {
            System.Console.WriteLine(s);
        }
    }
}
