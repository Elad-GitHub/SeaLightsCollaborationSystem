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
        private readonly NoteController _controller;
        private readonly INoteDataService _service;

        public NoteControllerTest()
        {
            _service = new MockNoteDataService();
            _controller = new NoteController(_service);
        }

        [Fact]
        public void GetNotes_WhenCalled_ReturnsOkResult()
        {
            var okResult = _controller.GetNotes();

            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }

        [Fact]
        public void GetNotes_WhenCalled_ReturnsAllItems()
        {
            var okResult = _controller.GetNotes() as OkObjectResult;

            var items = Assert.IsType<List<Note>>(okResult.Value);

            Assert.Equal(2, items.Count);
        }

        [Fact]
        public void GetNoteById_UnknownIdPassed_ReturnsNotFoundResult()
        {
            var notFoundResult = _controller.GetNoteById(3);
            
            Assert.IsType<NotFoundResult>(notFoundResult);
        }

        [Fact]
        public void GetNoteById_ExistingIdPassed_ReturnsOkResult()
        {
            var okResult = _controller.GetNoteById(1);

            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }

        [Fact]
        public void GetNoteById_ExistingIDPassed_ReturnsRightItem()
        {
            var okResult = _controller.GetNoteById(1) as OkObjectResult;
            
            Assert.IsType<Note>(okResult.Value);
            
            Assert.Equal(1, (okResult.Value as Note).Id);
        }
    }
}
