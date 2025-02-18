using Notes.Views;


namespace Notes;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		// Register pages for navigation
        Routing.RegisterRoute(nameof(NotePage), typeof(NotePage));
		
	}
}
