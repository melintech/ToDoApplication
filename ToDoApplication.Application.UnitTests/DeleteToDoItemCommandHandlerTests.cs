using Application.Use_Cases.CommandHandlers;
using Application.Use_Cases.Commands;
using Domain.Repositories;
using NSubstitute;
using Xunit;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ToDoApplication.Application.UnitTests
{
    public class DeleteToDoItemCommandHandlerTests
    {
        private readonly IToDoItemRepository repository;
        private readonly Guid itemId;

        public DeleteToDoItemCommandHandlerTests()
        {
            repository = Substitute.For<IToDoItemRepository>();
            itemId = Guid.NewGuid();
        }

        [Fact]
        public async Task Given_DeleteToDoItemCommandHandler_When_HandleIsCalled_Then_ToDoItemShouldBeDeleted()
        {
            // Arrange
            var command = new DeleteToDoItemCommand { Id = itemId };
            repository.GetByIdAsync(itemId).Returns(new Domain.Entities.ToDoItem { Id = itemId });

            var handler = new DeleteToDoItemCommandHandler(repository);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(itemId, result);
            await repository.Received(1).DeleteAsync(itemId);
        }

        [Fact]
        public async Task Given_DeleteToDoItemCommandHandler_When_ItemDoesNotExist_Then_ExceptionShouldBeThrown()
        {
            // Arrange
            var command = new DeleteToDoItemCommand { Id = itemId };
            repository.GetByIdAsync(itemId).Returns((Domain.Entities.ToDoItem)null);

            var handler = new DeleteToDoItemCommandHandler(repository);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => handler.Handle(command, CancellationToken.None));
        }
    }
}
