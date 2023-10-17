using ItemmApp.ViewModel;

namespace ItemmApp;

public partial class PersonalDepartmentUpdateCadasterPage : ContentPage
{
    private PersonalDepartmentUpdateCadasterViewModel _vm;
    public PersonalDepartmentUpdateCadasterPage(PersonalDepartmentUpdateCadasterViewModel vm)
	{
		InitializeComponent();
        BindingContext = _vm = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _vm.InitAsync();
    }

}

