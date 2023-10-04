using ItemmApp.ViewModel;

namespace ItemmApp;

public partial class PersonalDepartmentAttendancePage : ContentPage
{
        
    public PersonalDepartmentAttendancePage(PersonalDepartmentAttendanceViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }

}

