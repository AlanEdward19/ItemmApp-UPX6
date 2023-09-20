using ItemmApp.ViewModel;

namespace ItemmApp;

public partial class PersonalDepartmentCadasterPage : ContentPage
{
        
    public PersonalDepartmentCadasterPage(PersonalDepartmentCadasterViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }

}

