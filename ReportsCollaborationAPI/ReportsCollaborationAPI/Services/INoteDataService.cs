using ReportsCollaborationAPI.Models;
using System.Collections.Generic;

namespace ReportsCollaborationAPI.Services
{
    public interface INoteDataService
    {
        Note GetNoteById(int noteId);
        
        List<Note> GetNotes();

        void AddNote(Note note);

        void AddNotes(List<Note> notes);

        void EditNote(Note note);

        void DeleteNote(Note note); 
    }
}
