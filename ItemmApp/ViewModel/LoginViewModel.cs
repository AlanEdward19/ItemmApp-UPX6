using System.Text;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using InventarioMobile.ViewModels;
using ItemmApp.Interfaces;
using ItemmApp.Models.Request;
using Microsoft.Toolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Input;
using ItemmApp.Helpers;
using ItemmApp.Validators;


namespace ItemmApp.ViewModel;

public partial class LoginViewModel : BaseViewModel
{
    [ObservableProperty] public bool isPassword = true;
    [ObservableProperty] public bool isEnabled = true;
    [ObservableProperty]
    string email;

    [ObservableProperty]
    string password;

    private readonly ILoginRepository _loginRepository;
    public LoginViewModel(ILoginRepository loginRepository)
    {
        _loginRepository = loginRepository;
    }

    [ICommand]
    public void TogglePasswordVisibility()
    {
        IsPassword = !IsPassword;
    }

    [RelayCommand]
    public async Task Login()
    {
        IsEnabled = false;
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
        IToast toast;

        if (result is null || string.IsNullOrEmpty(result.token))
        {
            toast = Toast.Make("Falha ao realizar login, tente novamente!", ToastDuration.Long);
            await toast.Show();
            return;
        }

        toast = Toast.Make("Login efetuado com sucesso!", ToastDuration.Long);
        await toast.Show();

        SessionHelper.SaveToken(result.token, DateTime.Now.AddMinutes(30));
        IsEnabled = true;

        await Shell.Current.GoToAsync(nameof(MainPage));
    }
}