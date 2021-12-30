using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReportsCollaborationAPI.Services;
using System;
using System.IO;
using System.Linq;
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
            //get all report user files
            var files = _fileDataService.GetFiles(parentId, collaboratorId);

            //filter only wnated file to download
            var existingFile = files.FirstOrDefault(file => file.Id == Id);

            if (existingFile == null)
            {
                return NotFound();
            }

            var bytes = await System.IO.File.ReadAllBytesAsync(existingFile.Link);

            return File(bytes, "text/plain", Path.GetFileName(existingFile.Link)); ; 
        }

        [HttpPost]
        [Route("[controller]/UploadFile/{parentId}/{collaboratorId}")]
        public async Task<IActionResult> UploadFile(int parentId, int collaboratorId, IFormFile file, CancellationToken cancellationToken)
        {
            if (!ValidateFileType(file))
            {
                return BadRequest(new { message = "Invalid File! The file upload has been blocked because its type can not be .exe / .js / .vbs" });
            }
            else if(!ValidateFileSize(file))
            {
                return BadRequest(new { message = "Invalid File! The file upload has been blocked because its size can not be bigger than 10 mb" });
            }

            await WriteFile(file, parentId, collaboratorId);

            return Ok();
        }

        //check if file size is less than 10 mb
        private bool ValidateFileSize(IFormFile file)
        {
            return (file.Length < 10000000);
        }

        //check if file type is valid
        private bool ValidateFileType(IFormFile file)
        {
            var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];

            return (!extension.Equals(".exe") && !extension.Equals(".js") && !extension.Equals(".vbs"));
        }

        private async Task<bool> WriteFile(IFormFile file, int parentId, int collaboratorId)
        {
            bool isSaveSuccess = false;
            
            try
            {
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];

                //Create a new Name for the file due to security reasons.
                string fileName = DateTime.Now.Ticks + extension; 

                var pathBuilt = Path.Combine(Directory.GetCurrentDirectory(), "Repository\\files");

                if (!Directory.Exists(pathBuilt))
                {
                    Directory.CreateDirectory(pathBuilt);
                }

                var path = Path.Combine(Directory.GetCurrentDirectory(), "Repository\\files", fileName);

                var fileToSave = new Models.File()
                {
                    Link = path,
                    ParentId = parentId,
                    CollaboratorId = collaboratorId
                };

                _fileDataService.AddFile(fileToSave);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                isSaveSuccess = true;
            }
            catch (Exception e)
            {
                //log error
            }

            return isSaveSuccess;
        }
    }
}
