using TodoList.Entities;
using TodoList.Models;

namespace TodoList.Services.Interfaces
{
    public interface ITodoItemService
    {
        Task<TodoItemDetailsModel?> GetByIdAsync(int todoListId, int id, CancellationToken cancellation);

        Task<bool> DeleteAsync(int todoListId, int id, CancellationToken cancellation);

        Task<TodoItemDetailsModel> AddAsync(int todoListId, TodoItemAddModel item, CancellationToken cancellation);

        Task<TodoItemDetailsModel?> UpdateAsync(int todoListId, int id, TodoItemAddModel item, CancellationToken cancellation);
    }
}
