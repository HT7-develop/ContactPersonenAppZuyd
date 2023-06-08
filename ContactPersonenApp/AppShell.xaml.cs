namespace ContactPersonenApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(NieuwContactPage), typeof(NieuwContactPage));
	}
}
