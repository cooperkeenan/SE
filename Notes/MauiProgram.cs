using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Notes.Data;            // For NotesDbContext
using Notes.ViewModels;      // For AllNotesViewModel and NoteViewModel
using Notes.Views;           // For AllNotesPage and NotePage

namespace Notes;

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

#if DEBUG
        builder.Logging.AddDebug();
#endif

        // Load configuration from embedded resource "Notes.appsettings.json"
        var assembly = Assembly.GetExecutingAssembly();
        using var stream = assembly.GetManifestResourceStream("Notes.appsettings.json");
        if (stream != null)
        {
            var config = new ConfigurationBuilder()
                .AddJsonStream(stream)
                .Build();
            builder.Configuration.AddConfiguration(config);
        }
        else
        {
            Console.WriteLine("Warning: Embedded resource 'Notes.appsettings.json' not found.");
        }

        // **** Add the following code here ****

        // Get the connection string from configuration
        var connectionString = builder.Configuration.GetConnectionString("LocalConnection");
        // Register the DbContext with SQL Server
        builder.Services.AddDbContext<NotesDbContext>(options =>
            options.UseSqlServer(connectionString));

        // Register ViewModels
        builder.Services.AddSingleton<AllNotesViewModel>();
        builder.Services.AddTransient<NoteViewModel>();

        // Register Views
        builder.Services.AddSingleton<AllNotesPage>();
        builder.Services.AddTransient<NotePage>();

        // ***************************************

        return builder.Build();
    }
}
