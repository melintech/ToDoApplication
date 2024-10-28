using Application.Use_Cases.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.Repositories;
using Application.DTOs;
using Application.Use_Cases.Queries;

namespace ToDoApplication.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ToDoItemsController : ControllerBase
    {
        private readonly IMediator mediator;

        private readonly IToDoItemRepository _toDoRepository;

        public ToDoItemsController(IMediator mediator, IToDoItemRepository toDoRepository)
        {
            this.mediator = mediator;
            this._toDoRepository = toDoRepository;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var toDoItem = await _toDoRepository.GetByIdAsync(id);
            if (toDoItem == null)
            {
                return NotFound();
            }
            return Ok(toDoItem);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateToDoItem(CreateToDoItemCommand command)
        {
            return await mediator.Send(command); 
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateToDoItem(Guid id, UpdateToDoItemCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("ID in the path does not match the ID in the command.");
            }

            var isUpdated = await mediator.Send(command);

            if (!isUpdated)
            {
                return NotFound();
            }

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Guid>> DeleteToDoItem(Guid id)
        {
            return await mediator.Send(new DeleteToDoItemCommand { Id = id });
        }

        [HttpGet]
        public async Task<ActionResult<List<ToDoItemDto>>> GetAllToDoItems()
        {
            return await mediator.Send(new GetAllToDoItemsQuery());
        }
    }
}
