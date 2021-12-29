using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReportsCollaborationAPI.Services;

namespace ReportsCollaborationAPI.Controllers
{
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly ILogger<NoteController> _logger;
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
                return NotFound();
            }

            return Ok(note);
        }
    }
}
