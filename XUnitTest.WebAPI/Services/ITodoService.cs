using XUnitTest.WebAPI.Data.Entities;

namespace XUnitTest.WebAPI.Services
{
    public interface ITodoService
    {
        Task<List<Todo>> GetAllAsync();
        Task SaveAsync(Todo newTodo);
    }
}