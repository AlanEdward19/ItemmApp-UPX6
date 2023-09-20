using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace ItemmApp.ViewModel;

public partial class LoginViewModel : ObservableObject
{
    [ICommand]
    async Task MoveToMain() => await Shell.Current.GoToAsync(nameof(MainPage));
}