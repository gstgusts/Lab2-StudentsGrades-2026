using System.Windows;
using Lab2.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Lab4.WpfStudents
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // Apply pending EF Core migrations once at startup (replaces EnsureCreated in the repository)
            using (var db = new StudentDbContext())
            {
                db.Database.Migrate();
            }

            base.OnStartup(e);
        }
    }

}
