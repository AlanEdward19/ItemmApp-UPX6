namespace ItemmApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        AppShell.SetNavBarIsVisible(this, false);

        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        Routing.RegisterRoute(nameof(PersonalDepartmentPage), typeof(PersonalDepartmentPage));
        Routing.RegisterRoute(nameof(PersonalDepartmentCadasterPage), typeof(PersonalDepartmentCadasterPage));
    }
}
