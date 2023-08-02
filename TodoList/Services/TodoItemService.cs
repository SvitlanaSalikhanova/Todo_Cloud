using AutoMapper;
using TodoList.Entities;
using TodoList.Models;
using TodoList.Repositories.Interfaces;
using TodoList.Services.Interfaces;

namespace TodoList.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly ITodoItemRepository _todoItemRpository;
        private readonly ITodoListRepository _todoListRpository;
        private readonly IMapper _mapper;

        public TodoItemService(ITodoItemRepository todoItemRepository, ITodoListRepository todoListRpository, IMapper mapper)
        {
            _todoItemRpository = todoItemRepository;
            _todoListRpository = todoListRpository;
            _mapper = mapper;
        }

        public async Task<TodoItemDetailsModel?> GetByIdAsync(int todoListId, int id, CancellationToken cancellation)
        {
            var todoList = await _todoListRpository.GetById(todoListId, cancellation);
            if (todoList == null)
            {
                return null;
            }
            var todoItem = todoList.TodoItems.Where(x => x.Id == id).FirstOrDefault();

            return _mapper.Map<TodoItemDetailsModel>(todoItem);
        }

        public async Task<bool> DeleteAsync(int todoListId, int id, CancellationToken cancellation)
        {
            var todoList = await _todoListRpository.GetById(todoListId, cancellation);
            var todoListItem = todoList?.TodoItems.Where(x => x.Id == id).FirstOrDefault();

            if (todoListItem == null)
            {
                return false;
            }

            return await _todoItemRpository.DeleteById(id, cancellation);
        }

        public async Task<TodoItemDetailsModel> AddAsync(int todoListId, TodoItemAddModel item, CancellationToken cancellation)
        {
            var todoItemEntity = new TodoItemEntity
            {
                Description = item.Description,
                TodoListId = todoListId,
            };
            var newItem = await _todoItemRpository.Add(todoItemEntity, cancellation);
            return _mapper.Map<TodoItemDetailsModel>(newItem);
        }

        public async Task<TodoItemDetailsModel?> UpdateAsync(int todoListId, int id,
            TodoItemAddModel item, CancellationToken cancellation)
        {
            var todoList = await _todoListRpository.GetById(todoListId, cancellation);
            var todoListItem = todoList?.TodoItems.Where(x => x.Id == id).FirstOrDefault();

            if (todoListItem == null)
            {
                return null;
            }
            var newItem = new TodoItemEntity
            {
                Description = item.Description,
                TodoListId = todoListId,
                IsDone = item.IsDone
            };

            var updatedItem = await _todoItemRpository.Update(id, newItem, cancellation);

            return _mapper.Map<TodoItemDetailsModel?>(updatedItem);
        }
    }
}
