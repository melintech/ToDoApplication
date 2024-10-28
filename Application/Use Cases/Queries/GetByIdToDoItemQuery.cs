using MediatR;
using Application.DTOs;
using System;

namespace Application.Use_Cases.Queries
{
    public class GetByIdToDoItemQuery : IRequest<ToDoItemDto>
    {
        public Guid Id { get; set; }

        public GetByIdToDoItemQuery(Guid id)
        {
            Id = id;
        }
    }
}
