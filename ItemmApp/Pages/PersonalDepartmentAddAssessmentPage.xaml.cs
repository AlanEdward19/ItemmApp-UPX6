using ItemmApp.ViewModel;

namespace ItemmApp;

public partial class PersonalDepartmentAddAssessmentPage : ContentPage
{

    public PersonalDepartmentAddAssessmentPage(PersonalDepartmentAddAssessmentViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }

}

