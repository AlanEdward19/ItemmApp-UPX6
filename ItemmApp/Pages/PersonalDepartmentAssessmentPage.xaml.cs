using ItemmApp.ViewModel;

namespace ItemmApp;

public partial class PersonalDepartmentAssessmentPage : ContentPage
{
        
    public PersonalDepartmentAssessmentPage(PersonalDepartmentAssessmentViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }

}

