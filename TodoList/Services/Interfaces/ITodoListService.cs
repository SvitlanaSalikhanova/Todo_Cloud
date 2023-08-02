using TodoList.Entities;
using TodoList.Models;

namespace TodoList.Services.Interfaces
{
    public interface ITodoListService
    {
        Task<TodoListDetailsModel?> GetByIdAsync(int id, CancellationToken cancellation);

        Task<List<TodoListDetailsModel>> GetAllAsync(CancellationToken cancellation);

        Task<bool> DeleteAsync(int id, CancellationToken cancellation);

        Task<TodoListDetailsModel> AddAsync(TodoListAddModel list, CancellationToken cancellation);

        Task<TodoListEntity?> UpdateNameAsync(int id, string name, CancellationToken cancellation);
    }
}
