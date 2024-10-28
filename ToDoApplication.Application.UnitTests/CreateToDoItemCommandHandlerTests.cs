using Application.DTOs;
using Application.Use_Cases.CommandHandlers;
using Application.Use_Cases.Commands;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using NSubstitute;

namespace ToDoApplication.Application.UnitTests
{
    public class CreateToDoItemCommandHandlerTests
    {
        private readonly IToDoItemRepository repository;
        private readonly IMapper mapper;
        private Guid id;

        public CreateToDoItemCommandHandlerTests()
        {
            repository = Substitute.For<IToDoItemRepository>();
            mapper = Substitute.For<IMapper>();
        }

        [Fact]
        public void Given_CreateToDoItemCommandHandler_When_HandleIsCalled_Then_AToDoItemShouldBeCreated()
        {
            // Arrange
            ToDoItem toDoItem = GenerateToDoItem();
            repository.AddAsync(toDoItem).Returns(id);
            var command = new CreateToDoItemCommand();
            GenerateToDoItemDto();
            // Act
            var handler = new CreateToDoItemCommandHandler(repository, mapper);
            var result = handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.NotNull(result);
        }

        private void GenerateToDoItemDto()
        {
            mapper.Map<ToDoItemDto>(id).Returns(new ToDoItemDto
            {
                Id = (Guid)id
            });
        }

        private ToDoItem GenerateToDoItem()
        {
            return new ToDoItem { Id = Guid.NewGuid(),
                Description = "Test Description",
                IsDone = false,
                DueDate = DateTime.Now };
        }
    }
}