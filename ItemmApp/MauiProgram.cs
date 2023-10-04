﻿using ItemmApp.ViewModel;
using Microsoft.Extensions.Logging;

namespace ItemmApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
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

        #endregion

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}