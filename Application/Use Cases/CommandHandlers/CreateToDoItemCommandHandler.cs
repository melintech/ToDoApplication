using Application.Use_Cases.Commands;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Application.Use_Cases.CommandHandlers
{
    public class CreateToDoItemCommandHandler : IRequestHandler<CreateToDoItemCommand, Guid>
    {
        private readonly IToDoItemRepository repository;
        private readonly IMapper mapper;

        public CreateToDoItemCommandHandler(IToDoItemRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<Guid> Handle(CreateToDoItemCommand request, CancellationToken cancellationToken)
        {
            CreateToDoItemCommandValidator validationRules = new CreateToDoItemCommandValidator();
            var validationResult = validationRules.Validate(request);
            if (!validationResult.IsValid)
            {
                var errorResult = new List<string>();
                foreach (var error in validationResult.Errors)
                {
                    errorResult.Add(error.ErrorMessage);
                }
                throw new ValidationException(errorResult.ToString());
            }
            var toDoItem = mapper.Map<ToDoItem>(request);   
            
            return await repository.AddAsync(toDoItem);
        }
    }
}
