using TodoSQLite.Views;

namespace TodoSQLite;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(NewContact), typeof(NewContact));
	}
}
