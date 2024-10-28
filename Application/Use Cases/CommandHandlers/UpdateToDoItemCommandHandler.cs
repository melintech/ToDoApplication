using Application.Use_Cases.Commands;
using AutoMapper;
using Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Application.Use_Cases.CommandHandlers
{
    public class UpdateToDoItemCommandHandler : IRequestHandler<UpdateToDoItemCommand, bool>
    {
        private readonly IToDoItemRepository repository;
        private readonly IMapper mapper;

        public UpdateToDoItemCommandHandler(IToDoItemRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<bool> Handle(UpdateToDoItemCommand request, CancellationToken cancellationToken)
        {
            var validationRules = new UpdateToDoItemCommandValidator();
            var validationResult = validationRules.Validate(request);
            if (!validationResult.IsValid)
            {
                var errorResult = new List<string>();
                foreach (var error in validationResult.Errors)
                {
                    errorResult.Add(error.ErrorMessage);
                }
                throw new ValidationException(string.Join(", ", errorResult));
            }

            var toDoItem = await repository.GetByIdAsync(request.Id);
            if (toDoItem == null)
            {
                return false;
            }

            mapper.Map(request, toDoItem);

            await repository.UpdateAsync(toDoItem);
            return true;
        }
    }
}
