using Microsoft.EntityFrameworkCore;
using XUnitTest.WebAPI.Data;
using XUnitTest.WebAPI.Data.Entities;

namespace XUnitTest.WebAPI.Services
{
    public class TodoService : ITodoService
    {
        private readonly TodoDbContext _context;

        public TodoService(TodoDbContext context)
        {
            _context = context;
        }

        public Task<List<Todo>> GetAllAsync()
        {
            return _context.Todo.ToListAsync();
        }

        public Task SaveAsync(Todo newTodo)
        {
            _context.Todo.Add(newTodo);

            return _context.SaveChangesAsync();
        }
    }
}