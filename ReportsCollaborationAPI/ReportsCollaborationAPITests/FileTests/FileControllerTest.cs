using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReportsCollaborationAPI.Application;
using ReportsCollaborationAPI.Controllers;
using ReportsCollaborationAPI.Domain;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace ReportsCollaborationAPITests
{
    public class FileControllerTest
    {
        private readonly FileController _fileController;
        private readonly IFileDataService _service;

        public FileControllerTest()
        {
            _service = new MockFileDataService();
            _fileController = new FileController(_service);
        }

        [Fact]
        public void GetFiles_WhenCalled_ReturnsOkResult()
        {
            var okResult = _fileController.GetFiles(1, 1);

            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }

        [Fact]
        public void GetFiles_WhenCalled_ReturnsAllItems()
        {
            var okResult = _fileController.GetFiles(1, 1) as OkObjectResult;

            var items = Assert.IsType<List<ReportsCollaborationAPI.Domain.File>>(okResult.Value);

            Assert.Equal(3, items.Count);
        }

        [Fact]
        public async Task DownloadFile_WhenCalled_ReturnsFileIfExistAsync()
        {
            var result = await _fileController.DownloadFile(1, 1, 1);

            Assert.IsType<FileContentResult>(result as FileContentResult);
        }

        [Fact]
        public async Task DownloadFile_WhenCalled_ReturnsNotFoundIfFileNotExistAsync()
        {
            var result = await _fileController.DownloadFile(4, 1, 1) as NotFoundResult;

            Assert.IsType<NotFoundResult>(result as NotFoundResult);
        }

        [Fact]
        public async Task UploadFile_InvalidObjectPassed_NoFile_ReturnsBadRequestAsync()
        {
            var file = new ReportsCollaborationAPI.Domain.File()
            {
                ParentId = 3,
                Link = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "FileTests\\TestFileRepository\\files\\1\\1\\637769406318521861_1_1.txt"),
                CollaboratorId = 3,
                Privacy = PrivacyLevel.Public
            };


            var badResponse = await _fileController.UploadFile(file.ParentId, file.CollaboratorId, null, default(System.Threading.CancellationToken));

            Assert.IsType<BadRequestObjectResult>(badResponse as BadRequestObjectResult);
        }

        [Fact]
        public async Task UploadFile_ValidObjectPassed_ReturnsBadRequestAsync()
        {
            var file = new ReportsCollaborationAPI.Domain.File()
            {
                ParentId = 3,
                Link = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "FileTests\\TestFileRepository\\files\\1\\1\\637769406318521861_1_1.txt"),
                CollaboratorId = 3,
                Privacy = PrivacyLevel.Public
            };

            using (var stream = System.IO.File.OpenRead(file.Link))
            {
                var formFile = new FormFile(stream, 0, stream.Length, string.Empty, Path.GetFileName(stream.Name))
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "text/plain"
                };

                var okResult = await _fileController.UploadFile(file.ParentId, file.CollaboratorId, formFile, default(System.Threading.CancellationToken));

                Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
            }
        }
    }
}
