using Microsoft.AspNetCore.Mvc;
using ReportsCollaborationAPI.Application;
using ReportsCollaborationAPI.Domain;

namespace ReportsCollaborationAPI.Controllers
{
    [ApiController]
    public class NoteController : ControllerBase
    {
        private INoteDataService _noteDataService;

        public NoteController(INoteDataService noteDataService)
        {
            _noteDataService = noteDataService;
        }

        [HttpGet]
        [Route("[controller]/GetNotes/{reportId}/{collaboratorId}")]
        public IActionResult GetNotes(int reportId, int collaboratorId)
        {
            //get all report user notes and public notes
            var notes = _noteDataService.GetNotes(reportId, collaboratorId);

            return Ok(notes);
        }

        [HttpPost]
        [Route("[controller]/AddNote")]
        public IActionResult AddNote(Note note)
        {
            if(note.Title == null)
            {
                return BadRequest("Title is Required");
            }

            //add a note to a given report
            _noteDataService.AddNote(note);

            return HttpContext != null ? 
                Created($"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}/{note.Id}", note) : Created(string.Empty, note);
        }
    }
}
