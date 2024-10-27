using System;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistence;

public class ToDoItemRepositoryTests
{
    private readonly ApplicationDbContext context;
    private readonly ToDoItemRepository repository;

    public ToDoItemRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;
        context = new ApplicationDbContext(options);

        context.ToDoItems.Add(new ToDoItem { Id = Guid.NewGuid(), Description = "Test item 1", DueDate = DateTime.Now, IsDone = false });
        context.ToDoItems.Add(new ToDoItem { Id = Guid.NewGuid(), Description = "Test item 2", DueDate = DateTime.Now, IsDone = true });
        context.SaveChanges();

        repository = new ToDoItemRepository(context);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsItem_WhenItemExists()
    {
        // arrange
        var existingItem = await context.ToDoItems.FirstAsync();

        // act
        var result = await repository.GetByIdAsync(existingItem.Id);

        // assert
        Assert.NotNull(result);
        Assert.Equal(existingItem.Id, result.Id);
    }

    [Fact]
    public async Task GetByIdAsync_ThrowsKeyNotFoundException_WhenItemDoesNotExist()
    {
        // Arrange
        var nonExistentId = Guid.NewGuid();

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => repository.GetByIdAsync(nonExistentId));
    }
}
