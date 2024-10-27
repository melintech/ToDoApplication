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
        // Configurăm contextul cu baza de date în memorie
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;
        context = new ApplicationDbContext(options);

        // Populăm baza de date cu date de test
        context.ToDoItems.Add(new ToDoItem { Id = Guid.NewGuid(), Description = "Test item 1", DueDate = DateTime.Now, IsDone = false });
        context.ToDoItems.Add(new ToDoItem { Id = Guid.NewGuid(), Description = "Test item 2", DueDate = DateTime.Now, IsDone = true });
        context.SaveChanges();

        // Instanțiem repository-ul cu contextul configurat
        repository = new ToDoItemRepository(context);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsItem_WhenItemExists()
    {
        // Arrange
        var existingItem = await context.ToDoItems.FirstAsync();

        // Act
        var result = await repository.GetByIdAsync(existingItem.Id);

        // Assert
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
