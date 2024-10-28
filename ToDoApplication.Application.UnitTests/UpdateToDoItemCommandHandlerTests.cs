using Application.Use_Cases.CommandHandlers;
using Application.Use_Cases.Commands;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using NSubstitute;

namespace ToDoApplication.Application.UnitTests
{
    public class UpdateToDoItemCommandHandlerTests
    {
        private readonly IToDoItemRepository repository;
        private readonly IMapper mapper;

        public UpdateToDoItemCommandHandlerTests()
        {
            repository = Substitute.For<IToDoItemRepository>();
            mapper = Substitute.For<IMapper>();
        }

        [Fact]
        public async Task Given_UpdateToDoItemCommandHandler_When_HandleIsCalled_Then_AToDoItemShouldBeUpdated()
        {
            // Arrange
            var id = Guid.NewGuid();
            var existingToDoItem = GenerateToDoItem(id);
            var command = new UpdateToDoItemCommand
            {
                Id = id,
                Description = "Updated Description",
                IsDone = true,
                DueDate = DateTime.Now
            };

            repository.GetByIdAsync(id).Returns(existingToDoItem);
            mapper.Map(command, existingToDoItem);

            // Act
            var handler = new UpdateToDoItemCommandHandler(repository, mapper);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result);
            await repository.Received(1).UpdateAsync(existingToDoItem);
        }

        private ToDoItem GenerateToDoItem(Guid id)
        {
            return new ToDoItem
            {
                Id = id,
                Description = "Test Description",
                IsDone = false,
                DueDate = DateTime.Now
            };
        }
    }
}
