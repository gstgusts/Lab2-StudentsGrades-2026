using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Lab2.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Lab4.WpfComponents
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private StudentDbContext _studentDbContext;
        public MainWindow()
        {
            InitializeComponent();
            _studentDbContext = new StudentDbContext();
        }

        private void Button_AddStudent_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(StudNameInput.Text.Trim()))
            {
                StudNameInput.Background = new SolidColorBrush(Colors.Red);
                return;
            }

            StudNameInput.Background = new SolidColorBrush(Colors.White);

            var student = new Student
            {
                Name = StudNameInput.Text,
                Surname = StudSurnameInput.Text,
                Code = StudNumberInput.Text
            };

            _studentDbContext.Students.Add(student);
            _studentDbContext.SaveChanges();
        }

        private void Button_Clear_Click(object sender, RoutedEventArgs e)
        {
            StudNameInput.Text = string.Empty;
            StudSurnameInput.Text = string.Empty;
            StudNumberInput.Text = string.Empty;
        }

        private void StudNameInput_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(StudNameInput.Text.Trim()))
            {
                StudNameInput.Background = new SolidColorBrush(Colors.White);
            }
        }

        private void SearchStudents_OnClick(object sender, RoutedEventArgs e)
        {
            bool failedOnly = FailedOnlyCheckBox.IsChecked == true;
            bool byName = SortByNameRadio.IsChecked == true;

            var students =_studentDbContext.Students
                .Include(s => s.Grades);

            var orderedStudents = byName ? students.OrderBy(s => s.Name) : students.OrderBy(s => s.Surname);

            if (failedOnly)
            {
                StudentList.ItemsSource = orderedStudents
                    .Where(s=>s.Grades.Any(g=>g.Value < 4))
                    .Select(s => new { s.Name, s.Surname, s.Code })
                    .ToList();
            }
            else
            {
                StudentList.ItemsSource = orderedStudents.Select(s => new { s.Name, s.Surname, s.Code })
                    .ToList();
            }
        }
    }
}