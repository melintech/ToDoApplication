using Application.DTOs;
using Application.Use_Cases.Queries;
using AutoMapper;
using Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Use_Cases.QuerieHandlers
{
    public class GetAllToDoItemsQueryHandler : IRequestHandler<GetAllToDoItemsQuery, List<ToDoItemDto>>
    {
        private readonly IToDoItemRepository repository;
        private readonly IMapper mapper;
        public GetAllToDoItemsQueryHandler(IToDoItemRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<List<ToDoItemDto>> Handle(GetAllToDoItemsQuery request, CancellationToken cancellationToken)
        {
            var items = await repository.GetAllAsync();
            return mapper.Map<List<ToDoItemDto>>(items);
            //return items.Select(item => mapper.Map<ToDoItemDto>(item)).ToList();
        }
    }
}
