using ReportsCollaborationAPI.Models;
using System.Collections.Generic;

namespace ReportsCollaborationAPI.Services
{
    public interface INoteDataService
    {
        //Note GetNoteById(int noteId);
        
        List<Note> GetNotes(int reportId, int collaboratorId);

        void AddNote(Note note);

        //void EditNote(Note note);

        //void DeleteNote(Note note); 
    }
}
