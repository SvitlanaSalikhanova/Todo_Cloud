using Microsoft.EntityFrameworkCore;
using TodoList.Entities;

namespace TodoList.Repositories.Interfaces
{
    public interface ITodoListRepository
    {
        Task<TodoListEntity?> GetById(int id, CancellationToken cancellation);

        Task<List<TodoListEntity>> GetAll(CancellationToken cancellation);

        Task<bool> DeleteById(int id, CancellationToken cancellation);

        Task<TodoListEntity> Add(TodoListEntity list, CancellationToken cancellation);

        Task<TodoListEntity?> Update(int id, string name, CancellationToken cancellation);
    }
}
