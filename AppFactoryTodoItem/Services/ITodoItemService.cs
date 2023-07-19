using AppFactoryTodoItem.Models;
using AppFactoryTodoItem.Repositories;

namespace AppFactoryTodoItem.Services
{
    public interface ITodoItemService
    {
        Task<List<TodoItem>> GetAll();
        Task<TodoItem> Get(Guid id);
        Task<TodoItem> Add(TodoItem todoItem);

        Task Delete(Guid id);
        Task Update(TodoItem todoItem);
    }

    public class TodoItemService : ITodoItemService
    {
        private ITodoItemRepository _todoItemRepository;
        public TodoItemService(ITodoItemRepository todoItemRepository)
        {
            _todoItemRepository = todoItemRepository;
        }
        public async Task<TodoItem> Add(TodoItem todoItem)
        {
            return await _todoItemRepository.Add(todoItem);
        }

        public async Task Delete(Guid id)
        {
            await _todoItemRepository.Delete(id);
        }

        public Task<TodoItem> Get(Guid id)
        {
            return _todoItemRepository.Get(id);
        }

        public async Task<List<TodoItem>> GetAll()
        {
            return await _todoItemRepository.GetAll();
        }

        public async Task Update(TodoItem todoItem)
        {
            await _todoItemRepository.Update(todoItem);
        }
    }
}
