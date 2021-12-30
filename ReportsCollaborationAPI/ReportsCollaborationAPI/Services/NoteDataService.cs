using Microsoft.EntityFrameworkCore;
using ReportsCollaborationAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReportsCollaborationAPI.Services
{
    public class NoteDataService : INoteDataService
    {
        private CollaborationSystemContext _Context; 

        public NoteDataService(CollaborationSystemContext noteContext)
        {
            _Context = noteContext;
        }

        //public Note GetNoteById(int noteId)
        //{
        //    var note = _Context.Notes.Find(noteId);

        //    _Context.Entry(note).State = EntityState.Detached;

        //    return note;
        //}

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

        //public void EditNote(Note note)
        //{
        //    _Context.Notes.Update(note);

        //    _Context.SaveChanges();
        //}

        //public void DeleteNote(Note note)
        //{
        //    _Context.Notes.Remove(note);

        //    _Context.SaveChanges();
        //}
    }
}
