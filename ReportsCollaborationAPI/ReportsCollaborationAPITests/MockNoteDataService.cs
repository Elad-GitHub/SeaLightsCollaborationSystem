using ReportsCollaborationAPI.Models;
using ReportsCollaborationAPI.Services;
using System.Collections.Generic;
using System.Linq;

namespace ReportsCollaborationAPITests
{
    //A Mock to represnt the note data service with fake data
    public class MockNoteDataService : INoteDataService
    {
        private List<Note> notes = new List<Note>()
        {
            new Note()
            {
                Id = 1,
                ParentId = 1,
                Title = "test title 1",
                Text = "test text 1",
                CollaboratorId = 1,
                Privacy = PrivacyLevel.Public
            },
            new Note()
            {
                Id = 2,
                ParentId = 1,
                Title = "test title 2",
                Text = "test text 2",
                CollaboratorId = 1,
                Privacy = PrivacyLevel.Private
            },
            new Note()
            {
                Id = 3,
                ParentId = 1,
                Title = "test title 3",
                Text = "test text 3",
                CollaboratorId = 2,
                Privacy = PrivacyLevel.Public
            },
            new Note()
            {
                Id = 4,
                ParentId = 1,
                Title = "test title 4",
                Text = "test text 4",
                CollaboratorId = 2,
                Privacy = PrivacyLevel.Private
            }
        };

        public List<Note> GetNotes(int reportId, int collaboratorId)
        {
            return notes.Where(note => note.ParentId == reportId && (note.Privacy == PrivacyLevel.Public ||
                                                (note.Privacy == PrivacyLevel.Private && note.CollaboratorId == collaboratorId))).ToList();
        }

        public void AddNote(Note note)
        {
            notes.Add(note);
        }
    }
}
