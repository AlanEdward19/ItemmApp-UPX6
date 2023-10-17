using CommunityToolkit.Maui;
using ItemmApp.Interfaces;
using ItemmApp.Repository;
using ItemmApp.ViewModel;
using Microsoft.Extensions.Logging;

namespace ItemmApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        #region Pages

        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddTransient<LoginViewModel>();

        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<MainViewModel>();

        builder.Services.AddTransient<PersonalDepartmentPage>();
        builder.Services.AddTransient<PersonalDepartmentViewModel>();

        builder.Services.AddTransient<PersonalDepartmentCadasterPage>();
        builder.Services.AddTransient<PersonalDepartmentCadasterViewModel>();

        builder.Services.AddTransient<PersonalDepartmentUpdateCadasterPage>();
        builder.Services.AddTransient<PersonalDepartmentUpdateCadasterViewModel>();

        builder.Services.AddTransient<PersonalDepartmentAttendancePage>();
        builder.Services.AddTransient<PersonalDepartmentAttendanceViewModel>();

        builder.Services.AddTransient<PersonalDepartmentAssessmentPage>();
        builder.Services.AddTransient<PersonalDepartmentAssessmentViewModel>();

        builder.Services.AddTransient<PersonalDepartmentAddAssessmentPage>();
        builder.Services.AddTransient<PersonalDepartmentAddAssessmentViewModel>();

        #endregion

        #region Services

        builder.Services.AddScoped<ILoginRepository, LoginRepository>();
        builder.Services.AddScoped<IStudentRepository, StudentRepository>();
        builder.Services.AddScoped<IClassRepository, ClassRepository>();
        builder.Services.AddScoped<IPoloRepository, PoloRepository>();
        builder.Services.AddScoped<ICompanyRepository, CompanyRepository>(); 
        builder.Services.AddScoped<IFunctionRepository, FunctionRepository>();
        builder.Services.AddScoped<IAssessmentRepository, AssessmentRepository>();
        builder.Services.AddScoped<IAttendanceRepository, AttendanceRepository>();

        #endregion

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
