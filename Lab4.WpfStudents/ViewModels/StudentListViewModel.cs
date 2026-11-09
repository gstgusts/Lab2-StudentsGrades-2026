using Lab2.DataAccess;
using Lab2.DataAccess.Interfaces;
using Lab2.DataAccess.Services;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace Lab4.WpfStudents.ViewModels
{
    public class StudentListViewModel : BaseViewModel
    {
        private readonly IStudentRepository _repo;
        public StudentListViewModel()
        {
            _repo = new StudentRepositoryService(new StudentDbContext());
            SearchStudentCommand = new RelayCommand(LoadStudents);
            SelectStudentCommand = new RelayCommand(LoadGrades);
        }

        private Student[] _students = Array.Empty<Student>();

        public Student[] Students
        {
            get => _students;
            set
            {
                _students = value;
                OnPropertyChanged();
            }
        }

        private string? _code;

        public string? Code
        {
            get => _code;
            set
            {
                if (_code == value) return;
                _code = value;
                OnPropertyChanged();
            }
        }

        private string? _name;

        public string? Name
        {
            get => _name;
            set
            {
                if (_name == value) return;
                _code = value;
                OnPropertyChanged();
            }
        }

        private string? _surname;

        public string? Surname
        {
            get => _surname;
            set
            {
                if (_surname == value) return;
                _surname = value;
                OnPropertyChanged();
            }
        }


        public void LoadStudents()
        {
            if(string.IsNullOrEmpty(Code) && string.IsNullOrEmpty(Name) && string.IsNullOrEmpty(Surname))
            {
                Students = _repo.GetAll().ToArray();
                IsCollectionEmpty = Students.Length == 0;
                return;
            }

            Students = _repo.Search(Code, Name, Surname).ToArray();
            IsCollectionEmpty = Students.Length == 0;
        }

        public ICommand SearchStudentCommand { get; }
        public ICommand SelectStudentCommand { get; }


        private bool? _isCollectionEmpty;

        public bool? IsCollectionEmpty
        {
            get => _isCollectionEmpty;
            set
            {
                if (_isCollectionEmpty == value) return;
                _isCollectionEmpty = value;
                OnPropertyChanged();
            }
        }
        public Student SelectedStudent { get; set; }

        private List<Grade> _grades = new List<Grade>();

        public List<Grade> Grades
        {
            get => _grades;

            set
            {
                _grades = value;
                OnPropertyChanged();
            }
        }

        public void LoadGrades()
        {
            if (SelectedStudent == null)
            {
                return;
            }

            Grades = _repo.GetGradesByStudentId(SelectedStudent.Id).ToList();
        }
    }
}
