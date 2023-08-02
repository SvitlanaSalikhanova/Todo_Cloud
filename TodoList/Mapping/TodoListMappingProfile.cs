using TodoList.Entities;
using TodoList.Models;
using AutoMapper;

namespace TodoList.Mapping
{
    public class TodoListMappingProfile : Profile
    {
        public TodoListMappingProfile()
        {
            CreateMap<TodoListEntity, TodoListDetailsModel>().ReverseMap();
            CreateMap<TodoListEntity, TodoListAddModel>().ReverseMap();
        }
    }
}
