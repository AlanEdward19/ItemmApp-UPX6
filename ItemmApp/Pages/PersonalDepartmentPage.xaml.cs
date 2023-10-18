using ItemmApp.Models.Response;
using ItemmApp.ViewModel;

namespace ItemmApp;

public partial class PersonalDepartmentPage : ContentPage
{
    private PersonalDepartmentViewModel _vm;
    public PersonalDepartmentPage(PersonalDepartmentViewModel vm)
	{
		InitializeComponent();
        BindingContext = _vm = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _vm.InitAsync();
    }

    private void InputView_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        throw new NotImplementedException();
    }
}

