using CommunityToolkit.Mvvm.Input;
using Notes.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Notes.ViewModels;

internal class NotesViewModel : IQueryAttributable
{
    public ObservableCollection<NoteViewModel> AllNotes { get; }
    public ICommand NewCommand { get; }
    public ICommand SelectNoteCommand { get; }

    public NotesViewModel()
    {
        AllNotes = new ObservableCollection<NoteViewModel>(
            Models.Note.LoadAll().Select(n => new NoteViewModel(n))
        );

        NewCommand = new AsyncRelayCommand(NewNoteAsync);
        SelectNoteCommand = new AsyncRelayCommand<NoteViewModel?>(SelectNoteAsync);
    }

    /// <summary>
    /// Opens a new blank note
    /// </summary>
    private async Task NewNoteAsync()
    {
        try
        {
            Console.WriteLine("Navigating to NotePage...");
            await Shell.Current.GoToAsync(nameof(Views.NotePage));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Navigation failed: {ex.Message}");
        }
    }

    /// <summary>
    /// Opens an existing note
    /// </summary>
    private async Task SelectNoteAsync(NoteViewModel? note)
    {
        if (note == null)
        {
            Console.WriteLine("SelectNoteAsync called with null note.");
            return;
        }

        try
        {
            Console.WriteLine($"Navigating to NotePage with note ID: {note.Identifier}");
            await Shell.Current.GoToAsync($"{nameof(Views.NotePage)}?load={note.Identifier}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Navigation failed: {ex.Message}");
        }
    }

    /// <summary>
    /// Handles query attributes passed via Shell navigation
    /// </summary>
    void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("deleted"))
        {
            if (query["deleted"] is string noteId && !string.IsNullOrEmpty(noteId))
            {
                NoteViewModel? matchedNote = AllNotes.FirstOrDefault(n => n.Identifier == noteId);
                if (matchedNote != null)
                {
                    AllNotes.Remove(matchedNote);
                    Console.WriteLine($"Note deleted: {noteId}");
                }
            }
        }
        else if (query.ContainsKey("saved"))
        {
            if (query["saved"] is string noteId && !string.IsNullOrEmpty(noteId))
            {
                NoteViewModel? matchedNote = AllNotes.FirstOrDefault(n => n.Identifier == noteId);
                if (matchedNote != null)
                {
                    matchedNote.Reload();
                    AllNotes.Move(AllNotes.IndexOf(matchedNote), 0);
                    Console.WriteLine($"Note updated: {noteId}");
                }
                else
                {
                    var loadedNote = Models.Note.Load(noteId);
                    if (loadedNote != null)
                    {
                        AllNotes.Insert(0, new NoteViewModel(loadedNote));
                        Console.WriteLine($"New note added: {noteId}");
                    }
                }
            }
        }
    }
}
