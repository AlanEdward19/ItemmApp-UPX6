using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace ItemmApp.ViewModel;

public partial class PersonalDepartmentViewModel : ObservableObject
{
    [ICommand]
    async Task MoveBack() => await Shell.Current.GoToAsync("..");

    [ICommand]
    async Task MoveToCadasterScreen() => await Shell.Current.GoToAsync(nameof(PersonalDepartmentCadasterPage));
}