using Application.Use_Cases.Commands;
using Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Use_Cases.CommandHandlers
{
    public class DeleteToDoItemCommandHandler : IRequestHandler<DeleteToDoItemCommand, Guid>
    {
        private readonly IToDoItemRepository repository;
        public DeleteToDoItemCommandHandler(IToDoItemRepository repository)
        {
            this.repository = repository;
        }
        public async Task<Guid> Handle(DeleteToDoItemCommand request, CancellationToken cancellationToken)
        {
            var book = await repository.GetByIdAsync(request.Id);
            if (book == null)
            {
                throw new Exception("ToDoItem not found");
            }
            await repository.DeleteAsync(request.Id);
            return request.Id;
        }
    }
}