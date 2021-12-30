using ReportsCollaborationAPI.Models;
using System.Collections.Generic;

namespace ReportsCollaborationAPI.Services
{
    public interface INoteDataService
    {
        List<Note> GetNotes(int reportId, int collaboratorId);

        void AddNote(Note note);
    }
}
