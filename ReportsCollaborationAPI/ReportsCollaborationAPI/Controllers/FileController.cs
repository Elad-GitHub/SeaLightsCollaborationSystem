using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReportsCollaborationAPI.Services;
using ReportsCollaborationAPI.Utilities;
using ReportsCollaborationAPI.Utilities.Contracts;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ReportsCollaborationAPI.Controllers
{
    [ApiController]
    public class FileController : ControllerBase
    {
        private IFileDataService _fileDataService;

        public FileController(IFileDataService fileDataService)
        {
            _fileDataService = fileDataService;
        }

        [HttpGet]
        [Route("[controller]/GetFiles/{parentId}/{collaboratorId}")]
        public IActionResult GetFiles(int parentId, int collaboratorId)
        {
            return Ok(_fileDataService.GetFiles(parentId, collaboratorId));
        }

        [HttpGet]
        [Route("[controller]/DownloadFile/{Id}/{parentId}/{collaboratorId}")]
        public async Task<IActionResult> DownloadFile(int Id, int parentId, int collaboratorId)
        {
            var file = await _fileDataService.GetFile(Id, parentId, collaboratorId);

            return file == null ? NotFound() : File(file.Item1, "text/plain", Path.GetFileName(file.Item2));
        }

        [HttpPost]
        [Route("[controller]/UploadFile/{parentId}/{collaboratorId}")]
        public async Task<IActionResult> UploadFile(int parentId, int collaboratorId, IFormFile file, CancellationToken cancellationToken)
        {
            if(file == null)
            {
                return BadRequest(new { message = "No file has been attached for upload opertaion" });
            }

            IValidator fileValidator = new FileValidator(file);

            var validationResult = fileValidator.Validate();

            var isValid = validationResult.Item1;

            var validationMessage = validationResult.Item2;
            
            if (!isValid)
            {
                return BadRequest(new { message = validationMessage });
            }
            
            try
            {
                await _fileDataService.SaveFile(file, parentId, collaboratorId);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok("File Uploaded");
        }
    }
}
