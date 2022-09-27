using Microsoft.EntityFrameworkCore;
using XUnitTest.WebAPI.Data.Entities;

namespace XUnitTest.WebAPI.Data
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
        {

        }
        public DbSet<Todo> Todo { get; set; } = default!;
    }
}