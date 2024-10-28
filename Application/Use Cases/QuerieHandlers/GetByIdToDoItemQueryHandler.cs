using Application.DTOs;
using Application.Use_Cases.Queries;
using AutoMapper;
using Domain.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Use_Cases.QueryHandlers
{
    public class GetByIdToDoItemQueryHandler : IRequestHandler<GetByIdToDoItemQuery, ToDoItemDto>
    {
        private readonly IToDoItemRepository repository;
        private readonly IMapper mapper;

        public GetByIdToDoItemQueryHandler(IToDoItemRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<ToDoItemDto> Handle(GetByIdToDoItemQuery request, CancellationToken cancellationToken)
        {
            var item = await repository.GetByIdAsync(request.Id);
            return item != null ? mapper.Map<ToDoItemDto>(item) : null;
        }
    }
}
