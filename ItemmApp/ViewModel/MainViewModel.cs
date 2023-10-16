using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using InventarioMobile.ViewModels;
using Microsoft.Toolkit.Mvvm.Input;

namespace ItemmApp.ViewModel;

public partial class MainViewModel : BaseViewModel
{
    [ICommand]
    async Task MoveToPersonalDepartment() => await Shell.Current.GoToAsync(nameof(PersonalDepartmentPage));
}