using Application.Use_Cases.Commands;
using Domain.Repositories;
using MediatR;

namespace Application.Use_Cases.CommandHandlers
{
    public class UpdateToDoItemCommandHandler : IRequestHandler<UpdateToDoItemCommand, bool>
    {
        private readonly IToDoItemRepository repository;

        public UpdateToDoItemCommandHandler(IToDoItemRepository repository)
        {
            this.repository = repository;
        }

        public async Task<bool> Handle(UpdateToDoItemCommand request, CancellationToken cancellationToken)
        {
            var toDoItem = await repository.GetByIdAsync(request.Id);
            if (toDoItem == null)
            {
                return false;
            }

            toDoItem.Description = request.Description;
            toDoItem.DueDate = request.DueDate;
            toDoItem.IsDone = request.IsDone;

            await repository.UpdateAsync(toDoItem);
            return true;
        }
    }
}
