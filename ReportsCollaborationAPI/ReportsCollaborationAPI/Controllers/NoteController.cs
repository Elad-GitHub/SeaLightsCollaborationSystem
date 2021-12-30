using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReportsCollaborationAPI.Models;
using ReportsCollaborationAPI.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportsCollaborationAPI.Controllers
{
    [ApiController]
    public class NoteController : ControllerBase
    {
        //private readonly ILogger<NoteController> _logger;
        private INoteDataService _noteDataService;

        public NoteController(INoteDataService noteDataService)//, ILogger<NoteController> logger)
        {
            //_logger = logger;
            _noteDataService = noteDataService;
        }

        [HttpGet]
        [Route("[controller]")]
        public IActionResult GetNotes()
        {
            return Ok(_noteDataService.GetNotes());
        }

        [HttpGet]
        [Route("[controller]/{noteId}")]
        public IActionResult GetNoteById(int noteId)
        {
            var note = _noteDataService.GetNoteById(noteId);

            if(note == null)
            {
                return NotFound($"Note with id: {noteId} was not found");
            }

            return Ok(note);
        }

        [HttpPost]
        [Route("[controller]")]
        public IActionResult AddNote(Note note)
        {
            if(note.Title == null)
            {
                return BadRequest("Title is Required");
            }

            _noteDataService.AddNote(note);

            return HttpContext != null ? 
                Created($"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}/{note.Id}", note) : Created($"", note);
        }

        [HttpPatch]
        [Route("[controller]/{noteId}")]
        public IActionResult EditNote(int noteId, Note note)
        {
            var existingNote = _noteDataService.GetNoteById(noteId);

            if (existingNote == null)
            {
                return NotFound($"Note with id: {noteId} was not found");
            }

            note.Id = existingNote.Id;

            _noteDataService.EditNote(note);

            return Ok();
        }

        [HttpDelete]
        [Route("[controller]/{noteId}")]
        public IActionResult DeleteNote(int noteId)
        {
            var note =_noteDataService.GetNoteById(noteId);

            if (note == null)
            {
                return NotFound($"Note with id: {noteId} was not found");
            }

            _noteDataService.DeleteNote(note);

            return Ok();
        }
    }
}
