using Notes.ViewModels;
using Microsoft.Maui.Controls;
namespace Notes.Views;
    
public partial class NotePage : ContentPage
{
    public NotePage()
    {
        this.BindingContext = new NoteViewModel();
        InitializeComponent();
    }
}
