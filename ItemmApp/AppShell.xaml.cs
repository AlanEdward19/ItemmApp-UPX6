﻿namespace ItemmApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        Routing.RegisterRoute(nameof(PersonalDepartmentPage), typeof(PersonalDepartmentPage));
        Routing.RegisterRoute(nameof(PersonalDepartmentCadasterPage), typeof(PersonalDepartmentCadasterPage));

        Routing.RegisterRoute(nameof(PersonalDepartmentUpdateCadasterPage), typeof(PersonalDepartmentUpdateCadasterPage));
        Routing.RegisterRoute(nameof(PersonalDepartmentAttendancePage), typeof(PersonalDepartmentAttendancePage));
        Routing.RegisterRoute(nameof(PersonalDepartmentAssessmentPage), typeof(PersonalDepartmentAssessmentPage));
        Routing.RegisterRoute(nameof(PersonalDepartmentAddAssessmentPage), typeof(PersonalDepartmentAddAssessmentPage));
    }
}
