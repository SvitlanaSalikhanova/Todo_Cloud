using Microsoft.EntityFrameworkCore;
using TodoList.Entities;
using TodoList.Repositories.Interfaces;

namespace TodoList.Repositories
{
    public class TodoListRepository : ITodoListRepository
    {
        private readonly AppDbContext _dbContext;
        public TodoListRepository(AppDbContext dbContext)
        {
            _dbContext= dbContext;
        }
        public async Task<TodoListEntity?> GetById(int id, CancellationToken cancellation)
        {
            var list = await _dbContext.TodoLists
                .Where(x => x.Id == id)
                .Include(x => x.TodoItems)
                .FirstOrDefaultAsync(cancellation);
            return list;
        }

        public async Task<List<TodoListEntity>> GetAll(CancellationToken cancellation)
        {
            var lists = await _dbContext.TodoLists
                .Include(x => x.TodoItems)
                .ToListAsync(cancellation);
            return lists;
        }

        public async Task<bool> DeleteById(int id, CancellationToken cancellation) 
        {
            var list = await GetById(id, cancellation);
            if (list != null)
            {
                var todoItems = list.TodoItems;

                _dbContext.TodoItems.RemoveRange(todoItems);

                _dbContext.TodoLists.Remove(list);
                await _dbContext.SaveChangesAsync(cancellation);
                return true;
            }

            return false;
        }

        public async Task<TodoListEntity> Add(TodoListEntity list, CancellationToken cancellation)
        {
            await _dbContext.TodoLists.AddAsync(list, cancellation);
            await _dbContext.SaveChangesAsync(cancellation);

            return list;

        }

        public async Task<TodoListEntity?> Update(int id, string name, CancellationToken cancellation)
        {
            var foundEntity = await GetById(id, cancellation);
            if (foundEntity != null) 
            {
                foundEntity.Name = name;
                await _dbContext.SaveChangesAsync(cancellation);
            }

            return foundEntity;
        }
    }
}
