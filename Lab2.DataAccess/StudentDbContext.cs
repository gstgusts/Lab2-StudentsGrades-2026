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

        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options) { }

        // Fallback for apps that create the context with "new StudentDbContext()" (Console, WPF, Lab3 API, EF tools).
        // When options are supplied through DI (Lab4.WebApi) they are used as-is.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured) return;

            optionsBuilder.UseSqlServer(
                $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={FindDatabaseFile()};Integrated Security=True");
        }

        // Walks up from the running app's bin folder to the solution and returns Lab2.DataAccess\Students.mdf,
        // so the path is not tied to one machine.
        private static string FindDatabaseFile()
        {
            var dir = new DirectoryInfo(AppContext.BaseDirectory);
            while (dir != null)
            {
                var dataAccessDir = Path.Combine(dir.FullName, "Lab2.DataAccess");
                if (Directory.Exists(dataAccessDir))
                {
                    return Path.Combine(dataAccessDir, "Students.mdf");
                }
                dir = dir.Parent;
            }

            throw new InvalidOperationException(
                "Could not locate the Lab2.DataAccess folder to find Students.mdf.");
        }
    }
}
