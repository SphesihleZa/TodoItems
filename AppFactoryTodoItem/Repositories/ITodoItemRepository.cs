using AppFactoryTodoItem.Data;
using AppFactoryTodoItem.Models;
using Microsoft.EntityFrameworkCore;

namespace AppFactoryTodoItem.Repositories
{
    public interface ITodoItemRepository
    {
        Task<List<TodoItem>> GetAll();
        Task<TodoItem> Get(Guid id);
        Task<TodoItem> Add(TodoItem todoItem);

        Task Delete (Guid id);
        Task Update(TodoItem todoItem);

        

    }

    public class TodoItemRepository : ITodoItemRepository
    {
        private readonly ApplicationDbContext _context;
        public TodoItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public  async Task<TodoItem> Add(TodoItem todoItem)
        {
            todoItem.ID = Guid.NewGuid();
            await _context.TodoItems.AddAsync(todoItem);
            await _context.SaveChangesAsync();
            return todoItem;
        }

        public async Task Delete(Guid id)
        {
            var todoItem =await Get(id);
            _context.TodoItems.Remove(todoItem);
            _context.SaveChangesAsync();
        }

        public async Task<TodoItem> Get(Guid id)
        {
            return await _context.TodoItems.Where(x=>x.ID == id).FirstOrDefaultAsync();
        }

        public async Task<List<TodoItem>> GetAll()
        {
            return await _context.TodoItems.ToListAsync();
        }

        public async Task Update(TodoItem todoItem)
        {
            _context.Update(todoItem);
            await _context.SaveChangesAsync();
        }
        private bool TodoItemExists(Guid id)
        {
            return (_context.TodoItems?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
