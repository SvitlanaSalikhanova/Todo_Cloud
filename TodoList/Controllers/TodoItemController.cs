using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Mvc;
using TodoList.Models;
using TodoList.Services.Interfaces;

namespace TodoList.Controllers
{
    [Route("todolists/{todoListId}/todoItems")]
    [ApiController]
    public class TodoItemController : ControllerBase
    {
        private readonly ITodoListService _todoListService;
        private readonly ITodoItemService _todoItemService;

        public TodoItemController(ITodoListService todoListService, ITodoItemService todoItemService)
        {
            _todoListService = todoListService;
            _todoItemService = todoItemService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<TodoItemDetailsModel>> GetById(int todoListId, int id, CancellationToken cancellation = default)
        {
            TodoItemDetailsModel? todoItem = await _todoItemService.GetByIdAsync(todoListId, id, cancellation);

            if (todoItem == null)
            {
                return NotFound();
            }

            return Ok(todoItem);
        }

        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<TodoItemDetailsModel>> AddItemToList(int todoListId, TodoItemAddModel item, CancellationToken cancellation = default)
        {
            TodoItemDetailsModel? todoItem = await _todoItemService.AddAsync(todoListId, item, cancellation);

            if (todoItem == null)
            {
                return BadRequest();
            }

            return Ok(todoItem);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<TodoItemDetailsModel>> DeleteById(int todoListId, int id, CancellationToken cancellation = default)
        {
            var deleted = await _todoItemService.DeleteAsync(todoListId, id, cancellation);

            return deleted ? Ok() : BadRequest();
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<TodoItemDetailsModel>> Update(int todoListId, int id, TodoItemAddModel item,
            CancellationToken cancellation = default)
        {
            var newItem = await _todoItemService.UpdateAsync(todoListId, id, item, cancellation);

            return newItem is null ? BadRequest() : Ok(newItem);
        }




    }
}
