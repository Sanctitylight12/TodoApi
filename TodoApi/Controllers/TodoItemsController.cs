

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Models;


namespace TodoApi.Controllers
{

    [Route("api/todos")]
    [ApiController]
    public class TodoItemsController:ControllerBase
    {
        private readonly TodoContext _context;
        
        public TodoItemsController(TodoContext context)
        {
            _context = context;
        }

        // 取得所有 todos（Read All）
        // GET: api/todos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            return await _context.TodoItems.ToListAsync();
        }

        // 取得單一的todos（Read One）
        // GET: api/todos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(int id)
        {
            var item = await _context.TodoItems.FindAsync(id);
            if(item == null)return NotFound();
            return Ok(item);

        }

        // 新增 todos（Create）
        // POST: api/todos
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem item)
        {
            _context.TodoItems.Add(item);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTodoItem), new { id = item.Id }, item);
        }

        //更新 todos（Update）
        // PUT: api/todos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(int id, TodoItem item)
        {
            if(id!=item.Id) return BadRequest();

            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.TodoItems.Any(e => e.Id == id))
                    return NotFound();
                else
                    throw;
            }
            return NoContent();

        }


        // 刪除
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(int id)
        {
            var item = await _context.TodoItems.FindAsync(id);
            if(item == null)return NotFound();

            _context.TodoItems.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }



    }
}
