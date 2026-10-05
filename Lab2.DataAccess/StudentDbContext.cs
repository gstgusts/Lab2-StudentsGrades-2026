using Microsoft.EntityFrameworkCore;

namespace Lab2.DataAccess
{
    public class StudentDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        public DbSet<Grade> Grades { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        public StudentDbContext()
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer(
                @"Data Source=(LocalDB)\MSSQLLocalDB;
                                AttachDbFilename=C:\Users\gusts\source\repos\Lab2\Lab2.DataAccess\Students.mdf;
                                Integrated Security=True");

    }
}
