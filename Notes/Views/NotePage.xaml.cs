namespace Notes.Views;


[QueryProperty(nameof(ItemId), nameof(ItemId))]
public partial class NotePage : ContentPage
{
   
    // Class Constructor
    public NotePage()
    {
        InitializeComponent();

        string appDataPath = FileSystem.AppDataDirectory;
        string randomFileName = $"{Path.GetRandomFileName()}.notes.txt";

        LoadNote(Path.Combine(appDataPath, randomFileName));
    }



    // Load Note
    private void LoadNote(string fileName)
    {
        Models.Note noteModel = new Models.Note();
        noteModel.Filename = fileName;

        if (File.Exists(fileName))
        {
            noteModel.Date = File.GetCreationTime(fileName);
            noteModel.Text = File.ReadAllText(fileName);
        }

        BindingContext = noteModel;
    }


    //Save Button
    private async void SaveButton_Clicked(object sender, EventArgs e)
{
    if (BindingContext is Models.Note note)
    {
        string fileName = note.Filename ?? Path.Combine(FileSystem.AppDataDirectory, "default.notes.txt");
        File.WriteAllText(fileName, TextEditor.Text);
        await Shell.Current.GoToAsync("..");
    }
}


    //Delete Button
    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is Models.Note note)
        {
            // Delete the file.
            if (File.Exists(note.Filename))
                File.Delete(note.Filename);
        }

        await Shell.Current.GoToAsync("..");
    }

    //Gets file name of note 
    public string ItemId
    {
        set { LoadNote(value); }
    }
}
