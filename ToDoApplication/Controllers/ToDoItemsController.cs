using Application.Use_Cases.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ToDoApplication.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ToDoItemsController : ControllerBase
    {
        private readonly IMediator mediator;

        public ToDoItemsController(IMediator mediator)
        {
            this.mediator = mediator;
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
    }
}
