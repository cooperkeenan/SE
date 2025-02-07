using Microsoft.Maui.ApplicationModel; // for Launcher

namespace MyMauiApp;

public partial class AboutPage : ContentPage
{
    public AboutPage()
    {
        InitializeComponent();
    }

    private async void LearnMore_Clicked(object sender, EventArgs e)
    {
        // Open a URL in the system browser
        await Launcher.Default.OpenAsync("https://aka.ms/maui");
    }
}
