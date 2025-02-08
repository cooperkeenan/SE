using Notes.Views;


namespace Notes;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        // Set the MainPage to an instance of AppShell so the Shell navigation appears.
        MainPage = new AppShell();
    }
}


