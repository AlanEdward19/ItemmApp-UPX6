using System.Text;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using InventarioMobile.ViewModels;
using ItemmApp.Interfaces;
using ItemmApp.Models.Request;
using Microsoft.Toolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Input;
using ItemmApp.Validators;


namespace ItemmApp.ViewModel;

public partial class LoginViewModel : BaseViewModel
{
    [ObservableProperty]
    string email;

    [ObservableProperty]
    string password;

    private readonly ILoginRepository _loginRepository;
    public LoginViewModel(ILoginRepository loginRepository)
    {
        _loginRepository = loginRepository;
    }

    [RelayCommand]
    public async Task Login()
    {
        var loginRequest = new LoginRequest(Email, Password);

        var contract = new LoginValidator(loginRequest);

        if (!contract.IsValid)
        {
            var messages = contract.Notifications.Select(x => x.Message);
            var sb = new StringBuilder();

            foreach (var message in messages)
                sb.Append($"{message}\n");

            await Shell.Current.DisplayAlert("Atenção", sb.ToString(), "OK");
            return;
        }

        var result = await _loginRepository.LoginAsync(loginRequest);

        if (result is null || string.IsNullOrEmpty(result.token))
        {
            var toast = Toast.Make("Falha ao realizar login, tente novamente!", CommunityToolkit.Maui.Core.ToastDuration.Long);
            await toast.Show();
            return;
        }

        Preferences.Set("token", result.token);
        await Shell.Current.GoToAsync(nameof(MainPage));
    }
}