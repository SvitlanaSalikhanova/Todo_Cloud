using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Mvc;
using TodoList.Models;
using TodoList.Services.Interfaces;

namespace TodoList.Controllers
{
    [Route("todolists")]
    [ApiController]
    public class TodoListController : ControllerBase
    {
        private readonly ITodoListService _todoListService;

        public TodoListController(ITodoListService todoListService)
        {
            _todoListService = todoListService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<TodoListDetailsModel>> GetById(int id, CancellationToken cancellation = default)
        {
            TodoListDetailsModel? todoList = await _todoListService.GetByIdAsync(id, cancellation);

            if (todoList == null)
            {
                return NotFound();
            }

            return Ok(todoList);
        }

        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<TodoListDetailsModel>>> GetAll(CancellationToken cancellation = default)
        {
            var todoList = await _todoListService.GetAllAsync(cancellation);

            return Ok(todoList);
        }

        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<List<TodoListDetailsModel>>> Add(TodoListAddModel model, CancellationToken cancellation = default)
        {
            var todoList = await _todoListService.AddAsync(model, cancellation);
            return CreatedAtAction(nameof(GetById), new { id = todoList.Id }, todoList);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<TodoListDetailsModel>>> Delete(int id, CancellationToken cancellation = default)
        {
            var deleted = await _todoListService.DeleteAsync(id, cancellation);

            return deleted ? Ok() : BadRequest();
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<TodoListDetailsModel>>> UpdateName(int id, string name, CancellationToken cancellation = default)
        {
            var updated = await _todoListService.UpdateNameAsync(id, name, cancellation);

            return updated is null ? BadRequest() : Ok();
        }


    }
}
