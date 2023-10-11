using ItemmApp.ViewModel;

namespace ItemmApp;

public partial class LoginPage : ContentPage
{

    public LoginPage(LoginViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}