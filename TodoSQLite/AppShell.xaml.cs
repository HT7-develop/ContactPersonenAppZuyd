using ContactPersonenApp.Views;

namespace ContactPersonenApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(NewContact), typeof(NewContact));
	}
}
