using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace ItemmApp.ViewModel
{
    public partial class LoginViewModel : ObservableObject
    {

        public LoginViewModel()
        {
            Senha = "";
            IsVisible = false;
            HideShowPassword = "_";
        }

        private string email;
        private string senha;
        private string hideShowPassword;
        private bool isVisible;
        
        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }

        public string Senha
        {
            get => senha;
            set => SetProperty(ref senha, value);
        }

        public string HideShowPassword
        {
            set
            {
                SetProperty(ref hideShowPassword, value);
            }
            get { return hideShowPassword; }
        }

        public bool IsVisible
        {
            set
            {
                SetProperty(ref isVisible, value);
            }
            get { return isVisible; }
        }



        [ICommand]
        async Task MoveToMain() => await Shell.Current.GoToAsync(nameof(MainPage));

        [ICommand]
        void ShowHidePassword()
        {
            IsVisible = !IsVisible;

            if (IsVisible)
            {
                HideShowPassword = "👁️";
            }

            else
            {
                HideShowPassword = "_";
            }
        }
    }
}