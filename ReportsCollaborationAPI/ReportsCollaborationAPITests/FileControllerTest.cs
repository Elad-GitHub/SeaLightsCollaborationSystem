using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReportsCollaborationAPI.Controllers;
using ReportsCollaborationAPI.Models;
using ReportsCollaborationAPI.Services;
using System.Collections.Generic;
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

            var items = Assert.IsType<List<File>>(okResult.Value);

            Assert.Equal(3, items.Count);
        }

        //[Fact]
        //public void UploadFile_InvalidObjectPassed_ReturnsBadRequest()
        //{
        //    var file = new File()
        //    {
        //    };

        //    _fileController.ModelState.AddModelError("Title", "Required");

        //    var badResponse = _fileController.UploadFile(file.ParentId, file.CollaboratorId, new FormFile(), default(System.Threading.CancellationToken));

        //    Assert.IsType<BadRequestObjectResult>(badResponse);
        //}

        //[Fact]
        //public void UploadFile_ValidObjectPassed_ReturnsCreatedResponse()
        //{
        //    var note = new Note()
        //    {
        //        Id = 5,
        //        ParentId = 2,
        //        Title = "test title 5",
        //        Text = "test text 5",
        //        CollaboratorId = 2,
        //        Privacy = PrivacyType.Private
        //    };

        //    var createdResponse = _fileController.UploadFile(file.ParentId, file.CollaboratorId, new FormFile(), default(System.Threading.CancellationToken));

        //    Assert.IsType<CreatedResult>(createdResponse);
        //}

        //[Fact]
        //public void UploadFile_ValidObjectPassed_ReturnedResponseHasCreatedItem()
        //{
        //    var note = new Note()
        //    {
        //        Id = 6,
        //        ParentId = 2,
        //        Title = "test title 6",
        //        Text = "test text 6",
        //        CollaboratorId = 2,
        //        Privacy = PrivacyType.Public
        //    };

        //    var createdResponse = _fileController.UploadFile(file.ParentId, file.CollaboratorId, new FormFile(), default(System.Threading.CancellationToken));

        //    var item = createdResponse.Value as Note;

        //    Assert.IsType<Note>(item);

        //    Assert.Equal("test title 6", item.Title);
        //}
    }
}
