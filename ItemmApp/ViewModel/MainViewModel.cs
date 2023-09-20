using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace ItemmApp.ViewModel;

public partial class MainViewModel : ObservableObject
{
    [ICommand]
    async Task MoveToPersonalDepartment() => await Shell.Current.GoToAsync(nameof(PersonalDepartmentPage));

    [ICommand]
    async Task MoveBack() => await Shell.Current.GoToAsync("..");
}