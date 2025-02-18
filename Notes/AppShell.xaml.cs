using Notes.Views;

namespace Notes;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Register pages for navigation
        Routing.RegisterRoute(nameof(AllNotesPage), typeof(AllNotesPage));
        Routing.RegisterRoute(nameof(NotePage), typeof(NotePage)); // ✅ ADD THIS
    }
}
