using Application.Use_Cases.Commands;
using Domain.Repositories;
using MediatR;

namespace Application.Use_Cases.CommandHandlers
{
    public class CreateToDoItemCommandHandler : IRequestHandler<CreateToDoItemCommand, Guid>
    {
        private readonly IToDoItemRepository repository;

        public CreateToDoItemCommandHandler(IToDoItemRepository repository)
        {
            this.repository = repository;
        }
        public async Task<Guid> Handle(CreateToDoItemCommand request, CancellationToken cancellationToken)
        {
            var toDoItem = new Domain.Entities.ToDoItem
            {
                Description = request.Description,
                DueDate = request.DueDate,
                IsDone = request.IsDone
            };
            return await repository.AddAsync(toDoItem);
        }
    }
}
