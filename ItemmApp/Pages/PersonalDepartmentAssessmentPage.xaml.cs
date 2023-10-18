using ItemmApp.ViewModel;

namespace ItemmApp;

public partial class PersonalDepartmentAssessmentPage : ContentPage
{
        private readonly PersonalDepartmentAssessmentViewModel _vm;
    public PersonalDepartmentAssessmentPage(PersonalDepartmentAssessmentViewModel vm)
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

