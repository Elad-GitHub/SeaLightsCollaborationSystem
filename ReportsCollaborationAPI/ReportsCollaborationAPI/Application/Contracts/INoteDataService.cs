using ReportsCollaborationAPI.Domain;
using System.Collections.Generic;

namespace ReportsCollaborationAPI.Application
{
    public interface INoteDataService
    {
        List<Note> GetNotes(int reportId, int collaboratorId);

        void AddNote(Note note);
    }
}
