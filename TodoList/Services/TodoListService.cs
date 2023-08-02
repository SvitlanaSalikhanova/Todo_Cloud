using AutoMapper;
using TodoList.Entities;
using TodoList.Models;
using TodoList.Repositories.Interfaces;
using TodoList.Services.Interfaces;

namespace TodoList.Services
{
    public class TodoListService : ITodoListService
    {
        private readonly ITodoListRepository _todoListRpository;

        private readonly IMapper _mapper;

        public TodoListService(ITodoListRepository todoListRepository, IMapper mapper)
        {
            _todoListRpository = todoListRepository;
            _mapper = mapper;
        }

        public async Task<TodoListDetailsModel?> GetByIdAsync(int id, CancellationToken cancellation)
        {
            var entity = await _todoListRpository.GetById(id, cancellation);
            return _mapper.Map<TodoListDetailsModel?>(entity);
        }

        public async Task<List<TodoListDetailsModel>> GetAllAsync(CancellationToken cancellation)
        {
            var listEntities = await _todoListRpository.GetAll(cancellation);
            return _mapper.Map<List<TodoListDetailsModel>>(listEntities);
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellation)
        {
            return await _todoListRpository.DeleteById(id, cancellation);
        }

        public async Task<TodoListDetailsModel> AddAsync(TodoListAddModel listModel, CancellationToken cancellation)
        {
            var listEntity = _mapper.Map<TodoListEntity>(listModel);

            var resultEntity = await _todoListRpository.Add(listEntity, cancellation);

            return _mapper.Map<TodoListDetailsModel>(resultEntity);
        }

        public async Task<TodoListEntity?> UpdateNameAsync(int id, string name, CancellationToken cancellation)
        {
            var list = await GetByIdAsync(id, cancellation);
            
            if (list == null)
                return null;
            
            list.Name = name;
            return await _todoListRpository.Update(id, name, cancellation) ;
        }


    }
}
