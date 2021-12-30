using Microsoft.EntityFrameworkCore;
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
        public List<Note> GetNotes()
        {
            return _noteContext.Notes.ToList();
        }

        public Note GetNoteById(int noteId)
        {
            var note = _noteContext.Notes.Find(noteId);

            _noteContext.Entry(note).State = EntityState.Detached;

            return note;
        }

        public void AddNote(Note note)
        {
            _noteContext.Notes.Add(note);

            _noteContext.SaveChanges();
        }

        public void EditNote(Note note)
        {
            _noteContext.Notes.Update(note);

            _noteContext.SaveChanges();
        }

        public void DeleteNote(Note note)
        {
            _noteContext.Notes.Remove(note);

            _noteContext.SaveChanges();
        }
    }
}
