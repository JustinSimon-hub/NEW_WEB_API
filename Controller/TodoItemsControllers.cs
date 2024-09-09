using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyToDoAPI.Data;
using MyToDoAPI.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoItemsController : ControllerBase
    {
        private readonly ToDoContext _context;

        public ToDoItemsController(ToDoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoItem>>> GetToDoItems()
        {
            return await _context.ToDoItems.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoItem>> GetToDoItem(int id)
        {
            var ToDoItem = await _context.ToDoItems.FindAsync(id);

            if (ToDoItem == null)
            {
                return NotFound();
            }

            return ToDoItem;
        }

        [HttpPost]
        public async Task<ActionResult<ToDoItem>> PostToDoItem(ToDoItem ToDoItem)
        {
            _context.ToDoItems.Add(ToDoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetToDoItem), new { id = ToDoItem.Id }, ToDoItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutToDoItem(int id, ToDoItem ToDoItem)
        {
            if (id != ToDoItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(ToDoItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToDoItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDoItem(int id)
        {
            var ToDoItem = await _context.ToDoItems.FindAsync(id);
            if (ToDoItem == null)
            {
                return NotFound();
            }

            _context.ToDoItems.Remove(ToDoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ToDoItemExists(int id)
        {
            return _context.ToDoItems.Any(e => e.Id == id);
        }
    }
}
