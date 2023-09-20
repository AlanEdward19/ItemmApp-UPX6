using ItemmApp.ViewModel;

namespace ItemmApp;

public partial class MainPage : ContentPage
{

    public MainPage(MainViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }

}

