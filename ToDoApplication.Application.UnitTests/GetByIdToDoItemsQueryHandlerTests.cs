using Application.DTOs;
using Application.Use_Cases.QuerieHandlers;
using Application.Use_Cases.Queries;
using Application.Use_Cases.QueryHandlers;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using NSubstitute;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ToDoApplication.Application.UnitTests
{
    public class GetByIdToDoItemsQueryHandlerTests
    {
        private readonly IToDoItemRepository repository;
        private readonly IMapper mapper;
        private readonly Guid testId;

        public GetByIdToDoItemsQueryHandlerTests()
        {
            repository = Substitute.For<IToDoItemRepository>();
            mapper = Substitute.For<IMapper>();
            testId = Guid.NewGuid();
        }

        [Fact]
        public void Given_GetByIdToDoItemQueryHandler_When_HandleIsCalled_Then_ToDoItemShouldBeReturned()
        {
            // Arrange
            Guid id = Guid.NewGuid(); 
            var query = new GetByIdToDoItemQuery(id); 
            var repository = Substitute.For<IToDoItemRepository>();
            var mapper = Substitute.For<IMapper>();

            // Act
            var handler = new GetByIdToDoItemQueryHandler(repository, mapper);
            var result = handler.Handle(query, CancellationToken.None); 

            // Assert
            Assert.NotNull(result);
        }

        private ToDoItem GenerateToDoItem()
        {
            return new ToDoItem
            {
                Id = testId,
                Description = "Test Description",
                IsDone = false,
                DueDate = DateTime.Now
            };
        }

        private ToDoItemDto GenerateToDoItemDto(ToDoItem toDoItem)
        {
            return new ToDoItemDto
            {
                Id = (Guid)toDoItem.Id,
                Description = toDoItem.Description,
                IsDone = toDoItem.IsDone,
                DueDate = toDoItem.DueDate
            };
        }
    }
}
