using ReportsCollaborationAPI.Application;
using ReportsCollaborationAPI.Domain;
using System.Collections.Generic;
using System.Linq;

namespace ReportsCollaborationAPI.Infrastructure
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
            return _Context.Notes.Where(note => note.ParentId == reportId && (note.Privacy == PrivacyLevel.Public || 
                                                (note.Privacy == PrivacyLevel.Private && note.CollaboratorId == collaboratorId))).ToList();
        }

        public void AddNote(Note note)
        {
            _Context.Notes.Add(note);

            _Context.SaveChanges();
        }
    }
}
