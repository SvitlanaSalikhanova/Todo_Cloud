using Microsoft.EntityFrameworkCore;
using TodoList.Entities;
using TodoList.Repositories.Interfaces;

namespace TodoList.Repositories
{
    public class TodoItemRepository : ITodoItemRepository
    {
        private readonly AppDbContext _dbContext;
        public TodoItemRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<TodoItemEntity?> GetById(int id, CancellationToken cancellation)
        {
            var item = await _dbContext.TodoItems.Where(x => x.Id == id).FirstOrDefaultAsync(cancellation);
            return item;
        }

        public async Task<bool> DeleteById(int id, CancellationToken cancellation)
        {
            var item = await GetById(id, cancellation);
            if (item != null)
            {
                _dbContext.TodoItems.Remove(item);
                await _dbContext.SaveChangesAsync(cancellation);
                return true;
            }

            return false;
        }

        public async Task<TodoItemEntity> Add(TodoItemEntity item, CancellationToken cancellation)
        {
            await _dbContext.TodoItems.AddAsync(item, cancellation);
            await _dbContext.SaveChangesAsync(cancellation);

            return item;

        }

        public async Task<TodoItemEntity?> Update(int id, TodoItemEntity newItem, CancellationToken cancellation)
        {
            var item = await GetById(id, cancellation);
            if (item != null)
            {
                item.TodoListId = newItem.TodoListId;
                item.IsDone = newItem.IsDone;
                item.Description = newItem.Description;

                _dbContext.TodoItems.Update(item);
                await _dbContext.SaveChangesAsync(cancellation);
            }
            return item;
        }
    }
}
