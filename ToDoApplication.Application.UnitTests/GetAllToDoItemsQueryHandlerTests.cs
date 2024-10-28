using Application.DTOs;
using Application.Use_Cases.CommandHandlers;
using Application.Use_Cases.Commands;
using Application.Use_Cases.QuerieHandlers;
using Application.Use_Cases.Queries;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApplication.Application.UnitTests
{
    public class GetAllToDoItemsQueryHandlerTests
    {
        private readonly IToDoItemRepository repository;
        private readonly IMapper mapper;
        private Guid id;

        public GetAllToDoItemsQueryHandlerTests()
        {
            repository = Substitute.For<IToDoItemRepository>();
            mapper = Substitute.For<IMapper>();
        }

        [Fact]
        public void Given_GetAllToDoItemsQueryHandlerTests_When_HandleIsCalled_Then_AListOfToDoItemsShouldBeReturned()
        {
            // Arrange
            List<ToDoItem> toDoItems = GenerateToDoItems();
            repository.GetAllAsync().Returns(toDoItems);
            var query = new GetAllToDoItemsQuery();
            GenerateToDoItemsDto(toDoItems);
            // Act
            var handler = new GetAllToDoItemsQueryHandler(repository, mapper);
            var result = handler.Handle(query, CancellationToken.None);
            // Assert
            Assert.NotNull(result);
        }

        private void GenerateToDoItemsDto(List<ToDoItem> toDoItems)
        {
            mapper.Map<List<ToDoItemDto>>(toDoItems).Returns(new List<ToDoItemDto>
            {
                new ToDoItemDto
                {
                    Id = (Guid)toDoItems[0].Id,
                    Description = toDoItems[0].Description,
                    IsDone = toDoItems[0].IsDone,
                    DueDate = toDoItems[0].DueDate
                },
                new ToDoItemDto
                {
                    Id = (Guid)toDoItems[1].Id,
                    Description = toDoItems[1].Description,
                    IsDone = toDoItems[1].IsDone,
                    DueDate = toDoItems[1].DueDate
                }
            });
        }

        private List<ToDoItem> GenerateToDoItems()
        {
            return new List<ToDoItem>
            {
                new ToDoItem{
                Id = Guid.NewGuid(),
                Description = "Test Description",
                IsDone = false,
                DueDate = DateTime.Now
            },
                new ToDoItem{
                Id = Guid.NewGuid(),
                Description = "Test Description 2",
                IsDone = true,
                DueDate = DateTime.Now
                }
            };
        }
    }
}
