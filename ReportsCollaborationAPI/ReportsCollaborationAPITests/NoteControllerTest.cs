using Microsoft.AspNetCore.Mvc;
using ReportsCollaborationAPI.Controllers;
using ReportsCollaborationAPI.Models;
using ReportsCollaborationAPI.Services;
using System.Collections.Generic;
using Xunit;

namespace ReportsCollaborationAPITests
{
    public class NoteControllerTest
    {
        private readonly NoteController _noteController;
        private readonly INoteDataService _service;

        public NoteControllerTest()
        {
            _service = new MockNoteDataService();
            _noteController = new NoteController(_service);
        }

        [Fact]
        public void GetNotes_WhenCalled_ReturnsOkResult()
        {
            var okResult = _noteController.GetNotes(1, 1);

            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }

        [Fact]
        public void GetNotes_WhenCalled_ReturnsAllItems()
        {
            var okResult = _noteController.GetNotes(1, 1) as OkObjectResult;

            var items = Assert.IsType<List<Note>>(okResult.Value);

            Assert.Equal(3, items.Count);
        }

        [Fact]
        public void AddNote_InvalidObjectPassed_ReturnsBadRequest()
        {
            var note = new Note()
            {
                Id = 5,
                Text = "test text 5"
            };

            _noteController.ModelState.AddModelError("Title", "Required");

            var badResponse = _noteController.AddNote(note);

            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public void AddNote_ValidObjectPassed_ReturnsCreatedResponse()
        {
            var note = new Note()
            {
                Id = 5,
                ParentId = 2,
                Title = "test title 5",
                Text = "test text 5",
                CollaboratorId = 2,
                Privacy = PrivacyType.Private
            };

            var createdResponse = _noteController.AddNote(note);

            Assert.IsType<CreatedResult>(createdResponse);
        }

        [Fact]
        public void AddNote_ValidObjectPassed_ReturnedResponseHasCreatedItem()
        {
            var note = new Note()
            {
                Id = 6,
                ParentId = 2,
                Title = "test title 6",
                Text = "test text 6",
                CollaboratorId = 2,
                Privacy = PrivacyType.Public
            };

            var createdResponse = _noteController.AddNote(note) as CreatedResult;
           
            var item = createdResponse.Value as Note;

            Assert.IsType<Note>(item);

            Assert.Equal("test title 6", item.Title);
        }
    }
}
