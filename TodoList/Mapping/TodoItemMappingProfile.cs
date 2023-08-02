using AutoMapper;
using TodoList.Entities;
using TodoList.Models;

namespace TodoList.Mapping
{
    public class TodoItemMappingProfile : Profile
    {
        public TodoItemMappingProfile()
        {
            CreateMap<TodoItemEntity, TodoItemDetailsModel>().ReverseMap();
            CreateMap<TodoItemEntity, TodoItemAddModel>().ReverseMap();
        }
    }
}
