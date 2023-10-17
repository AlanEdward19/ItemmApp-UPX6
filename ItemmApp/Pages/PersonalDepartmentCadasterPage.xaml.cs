using ItemmApp.ViewModel;

namespace ItemmApp;

public partial class PersonalDepartmentCadasterPage : ContentPage
{
    private readonly PersonalDepartmentCadasterViewModel _vm;
    public PersonalDepartmentCadasterPage(PersonalDepartmentCadasterViewModel vm)
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

