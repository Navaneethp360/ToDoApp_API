using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ToDoAppApi.Data;
using ToDoAppApi.DTOs;
using ToDoAppApi.Models;

namespace ToDoAppApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // 👈 Ensures only logged-in users can access
    public class TodoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TodoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/todo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetUserTodos()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            return await _context.TodoItems
                .Where(t => t.UserId == userId)
                .ToListAsync();
        }

        // POST: api/todo
        [HttpPost]
        public async Task<ActionResult> CreateTodo([FromBody] CreateTodoDto dto)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var todo = new TodoItem
            {
                Title = dto.Title,
                IsCompleted = false,
                UserId = userId
            };

            _context.TodoItems.Add(todo);
            await _context.SaveChangesAsync();

            return Ok(todo);
        }

        // PUT: api/todo/5
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTodo(int id, [FromBody] UpdateTodoDto dto)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var todo = await _context.TodoItems.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
            if (todo == null)
                return NotFound();

            todo.Title = dto.Title;
            todo.IsCompleted = dto.IsCompleted;

            await _context.SaveChangesAsync();

            return Ok(todo);
        }


        // DELETE: api/todo/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTodo(int id)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var todo = await _context.TodoItems.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
            if (todo == null)
                return NotFound();

            _context.TodoItems.Remove(todo);
            await _context.SaveChangesAsync();

            return Ok("Todo deleted.");
        }
    }
}
