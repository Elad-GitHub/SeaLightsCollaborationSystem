using ReportsCollaborationAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace ReportsCollaborationAPI.Services
{
    public class NoteDataService : INoteDataService
    {
        private CollaborationSystemContext _Context; 

        public NoteDataService(CollaborationSystemContext context)
        {
            _Context = context;
        }

        public List<Note> GetNotes(int reportId, int collaboratorId)
        {
            return _Context.Notes.Where(note => note.ParentId == reportId && (note.Privacy == PrivacyType.Public || 
                                                (note.Privacy == PrivacyType.Private && note.CollaboratorId == collaboratorId))).ToList();
        }

        public void AddNote(Note note)
        {
            _Context.Notes.Add(note);

            _Context.SaveChanges();
        }
    }
}
