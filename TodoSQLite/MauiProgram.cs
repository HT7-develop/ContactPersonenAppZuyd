using Microsoft.Extensions.DependencyInjection.Extensions;
using ContactPersonenApp.Data;
using ContactPersonenApp.Views;

namespace ContactPersonenApp;

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

		builder.Services.AddSingleton<ContactList>();
		builder.Services.AddTransient<NewContact>();

		builder.Services.AddSingleton<ContactsDatabase>();

		return builder.Build();
	}
}
