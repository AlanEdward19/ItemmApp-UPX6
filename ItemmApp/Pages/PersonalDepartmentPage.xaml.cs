using ItemmApp.ViewModel;

namespace ItemmApp;

public partial class PersonalDepartmentPage : ContentPage
{

    public PersonalDepartmentPage(PersonalDepartmentViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }

}

