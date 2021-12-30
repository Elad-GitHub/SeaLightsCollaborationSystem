using ReportsCollaborationAPI.Models;
using ReportsCollaborationAPI.Services;
using System.Collections.Generic;
using System.Linq;

namespace ReportsCollaborationAPITests
{
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
                Privacy = PrivacyType.Public
            },
            new Note()
            {
                Id = 2,
                ParentId = 1,
                Title = "test title 2",
                Text = "test text 2",
                CollaboratorId = 1,
                Privacy = PrivacyType.Private
            },
            new Note()
            {
                Id = 3,
                ParentId = 1,
                Title = "test title 3",
                Text = "test text 3",
                CollaboratorId = 2,
                Privacy = PrivacyType.Public
            },
            new Note()
            {
                Id = 4,
                ParentId = 1,
                Title = "test title 4",
                Text = "test text 4",
                CollaboratorId = 2,
                Privacy = PrivacyType.Private
            }
        };

        //public Note GetNoteById(int noteId)
        //{
        //    return notes.Find(note => note.Id == noteId);
        //}

        public List<Note> GetNotes(int reportId, int collaboratorId)
        {
            return notes.Where(note => note.ParentId == reportId && (note.Privacy == PrivacyType.Public ||
                                                (note.Privacy == PrivacyType.Private && note.CollaboratorId == collaboratorId))).ToList();
        }

        public void AddNote(Note note)
        {
            notes.Add(note);
        }

        //public void EditNote(Note note)
        //{
        //    var existingNote = GetNoteById(note.Id.Value);

        //    existingNote.Title = note.Title;
        //    existingNote.Text = note.Text;
        //}

        //public void DeleteNote(Note note)
        //{
        //    notes.Remove(note);
        //}
    }
}
