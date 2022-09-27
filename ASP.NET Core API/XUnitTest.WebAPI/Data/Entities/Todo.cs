using System.Diagnostics.CodeAnalysis;

namespace XUnitTest.WebAPI.Data.Entities
{
    public class Todo
    {
        public int Id { get; set; }
        public string ItemName { get; set; } = default!;
        public bool IsCompleted { get; set; }
    }
}