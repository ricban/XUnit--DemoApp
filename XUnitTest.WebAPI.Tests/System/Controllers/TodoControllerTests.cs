using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using XUnitTest.WebAPI.Controllers;
using XUnitTest.WebAPI.Data.Entities;
using XUnitTest.WebAPI.Services;
using XUnitTest.WebAPI.Tests.MockData;

namespace XUnitTest.WebAPI.Tests.System.Controllers
{
    /*
        HTTP Status Code

        200  OK
        204  No Content
        500  Internal Server Error
    */

    public class TodoControllerTests
    {
        [Fact]
        public async Task GetAllAsync_CountGreaterThanZero_ShouldReturn200HttpStatusCode()
        {
            /// Arrange
            var todoService = new Mock<ITodoService>();
            todoService.Setup(_ => _.GetAllAsync()).ReturnsAsync(TodoMockData.GetTodos());
            var sut = new TodoController(todoService.Object);

            
            /// Act
            var result = (OkObjectResult)await sut.GetAllAsync();

            /// Assert
            result.StatusCode.Should().Be(200);
            todoService.Verify(_ => _.GetAllAsync(), Times.AtMostOnce());
        }

        [Fact]
        public async Task GetAllAsync_CountEqualsZero_ShouldReturn204HttpStatusCode()
        {
            /// Arrange
            var todoService = new Mock<ITodoService>();
            todoService.Setup(_ => _.GetAllAsync()).ReturnsAsync(TodoMockData.GetEmptyTodos());
            var sut = new TodoController(todoService.Object);

            /// Act
            var result = (NoContentResult)await sut.GetAllAsync();

            /// Assert
            result.StatusCode.Should().Be(204);
            todoService.Verify(_ => _.GetAllAsync(), Times.AtMostOnce());
        }

        [Fact]
        public async Task GetAllAsync_ThrowsException_ShouldReturn500HttpStatusCode()
        {
            /// Arrange
            var todoService = new Mock<ITodoService>();
            todoService.Setup(_ => _.GetAllAsync()).Throws<Exception>();
            var sut = new TodoController(todoService.Object);

            /// Act
            var result = (ObjectResult)await sut.GetAllAsync();
            
            /// Assert
            result.StatusCode.Should().Be(500);
            todoService.Verify(_ => _.GetAllAsync(), Times.AtMostOnce());
        }

        // Optional
        [Fact]
        public async Task SaveAsync_ShouldCallITodoServiceSaveAsync_AtleastOnce()
        {
            /// Arrange
            var todoService = new Mock<ITodoService>();
            var newTodo = TodoMockData.NewTodo();
            var sut = new TodoController(todoService.Object);

            /// Act
            var result = await sut.SaveAsync(newTodo);

            /// Assert
            todoService.Verify(_ => _.SaveAsync(newTodo), Times.Exactly(1));
        }

        [Fact]
        public async Task SaveAsync_SuccessfulSave__ShouldReturn200HttpStatusCode()
        {
            /// Arrange
            var todoService = new Mock<ITodoService>();
            var newTodo = TodoMockData.NewTodo();
            var sut = new TodoController(todoService.Object);

            /// Act
            var result = (OkResult)await sut.SaveAsync(newTodo);

            /// Assert
            result.StatusCode.Should().Be(200);
            todoService.Verify(_ => _.SaveAsync(newTodo), Times.AtMostOnce());
        }

        [Fact]
        public async Task SaveAsync_ThrowsException_ShouldReturn500HttpStatusCode()
        {
            
            /// Arrange
            var todoService = new Mock<ITodoService>();
            var newTodo = TodoMockData.NewTodo();
            todoService.Setup(_ => _.SaveAsync(newTodo)).Throws<Exception>();
            var sut = new TodoController(todoService.Object);

            /// Act
            var result = (ObjectResult)await sut.SaveAsync(newTodo);

            /// Assert
            result.StatusCode.Should().Be(500);
            todoService.Verify(_ => _.SaveAsync(newTodo), Times.AtMostOnce());
        }
    }
}