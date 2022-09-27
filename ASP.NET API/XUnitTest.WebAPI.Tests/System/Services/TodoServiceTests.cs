using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using XUnitTest.WebAPI.Data;
using XUnitTest.WebAPI.Services;
using XUnitTest.WebAPI.Tests.MockData;

namespace XUnitTest.WebAPI.Tests.System.Services
{
    public class TodoServiceTests : IDisposable
    {
        protected readonly TodoDbContext _context;

        public TodoServiceTests()
        {
            var options = new DbContextOptionsBuilder<TodoDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new TodoDbContext(options);

            _context.Database.EnsureCreated();
        }

        [Fact]
        public async Task GetAllAsync_ReturnTodoCollection()
        {
            /// Arrange
            LoadTodoMockData();

            var sut = new TodoService(_context);

            /// Act
            var result = await sut.GetAllAsync();

            /// Assert
            result.Should().HaveCount(TodoMockData.GetTodos().Count);
        }

        [Fact]
        public async Task SaveAsync_AddNewTodo()
        {
            /// Arrange
            LoadTodoMockData();

            var sut = new TodoService(_context);

            /// Act
            await sut.SaveAsync(TodoMockData.NewTodo());

            ///Assert
            int expectedRecordCount = TodoMockData.GetTodos().Count + 1;
            (await _context.Todo.CountAsync()).Should().Be(expectedRecordCount);
        }

        private void LoadTodoMockData()
        {
            _context.Todo.AddRange(MockData.TodoMockData.GetTodos());
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}