using ItemmApp.ViewModel;

namespace ItemmApp;

public partial class PersonalDepartmentUpdateCadasterPage : ContentPage
{
        
    public PersonalDepartmentUpdateCadasterPage(PersonalDepartmentUpdateCadasterViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }

}

