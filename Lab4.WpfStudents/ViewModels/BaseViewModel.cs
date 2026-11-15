using CommunityToolkit.Mvvm.ComponentModel;

namespace Lab4.WpfStudents.ViewModels
{
    // ObservableObject already implements INotifyPropertyChanged and provides
    // OnPropertyChanged([CallerMemberName]) and SetProperty(ref field, value).
    public class BaseViewModel : ObservableObject
    {
    }
}
