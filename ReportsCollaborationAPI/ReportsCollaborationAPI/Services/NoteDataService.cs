using ReportsCollaborationAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReportsCollaborationAPI.Services
{
    public class NoteDataService : INoteDataService
    {
        private NoteContext _noteContext; 

        public NoteDataService(NoteContext noteContext)
        {
            _noteContext = noteContext;
        }

        public void AddNote(Note note)
        {
            throw new NotImplementedException();
        }

        public void AddNotes(List<Note> notes)
        {
            throw new NotImplementedException();
        }

        public void DeleteNote(Note note)
        {
            throw new NotImplementedException();
        }

        public void EditNote(Note note)
        {
            throw new NotImplementedException();
        }

        public Note GetNoteById(int noteId)
        {
            return _noteContext.Notes.Find(noteId);
        }

        public List<Note> GetNotes()
        {
            return _noteContext.Notes.ToList();
        }
    }
}
