using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XUnitTest.WebAPI.Data.Entities;
using XUnitTest.WebAPI.Services;

namespace XUnitTest.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [Route("get-all")]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var result = await _todoService.GetAllAsync();

                if (result.Count == 0)
                {
                    return NoContent();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("save")]
        public async Task<IActionResult> SaveAsync(Todo newTodo)
        {
            try
            {
                await _todoService.SaveAsync(newTodo);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}