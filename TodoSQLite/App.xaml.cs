using ContactPersonenApp.Converters;

namespace ContactPersonenApp;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
        Resources.Add("ImagePathToImageSourceConverter", new ImagePathToImageSourceConverter());
        MainPage = new AppShell();
	}
}
