using ReportsCollaborationAPI.Models;
using ReportsCollaborationAPI.Services;
using System.Collections.Generic;

namespace ReportsCollaborationAPITests
{
    public class MockNoteDataService : INoteDataService
    {
        private List<Note> notes = new List<Note>()
        {
            new Note()
            {
                Id = 1,
                ReportId = 11,
                Title = "test title",
                Text = "test text",
                CollaboratorId = 1
            },
            new Note()
            {
                Id = 2,
                ReportId = 11,
                Title = "test title2",
                Text = "test text2",
                CollaboratorId = 2
            }
        };

        public void AddNote(Note note)
        {
            notes.Add(note);
        }

        public void DeleteNote(Note note)
        {
            notes.Remove(note);
        }

        public void EditNote(Note note)
        {
            var existingNote = GetNoteById(note.Id.Value);
            
            existingNote.Title = note.Title;
            existingNote.Text = note.Text;
        }

        public Note GetNoteById(int noteId)
        {
            return notes.Find(note => note.Id == noteId);
        }

        public List<Note> GetNotes()
        {
            return notes;
        }
    }
}
