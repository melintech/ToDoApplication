using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories
{
    public class ToDoItemRepository : IToDoItemRepository
    {
        private readonly ApplicationDbContext context;

        public ToDoItemRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<Guid> AddAsync(ToDoItem toDoItem)
        {
            await context.ToDoItems.AddAsync(toDoItem);
            await context.SaveChangesAsync();
            return toDoItem.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            var item = await context.ToDoItems.FindAsync(id);
            if (item != null)
            {
                context.ToDoItems.Remove(item);
                await context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("ToDoItem not found");
            }
        }


        public async Task<ToDoItem> GetByIdAsync(Guid id)
        {
            var item = await context.ToDoItems.FirstOrDefaultAsync(item => item.Id == id);
            if (item == null)
            {
                throw new KeyNotFoundException($"Item with Id '{id}' not found.");
            }
            return item;
        }

        public async Task UpdateAsync(ToDoItem item)
        {
            var existingItem = await context.ToDoItems.FindAsync(item.Id);
            if (existingItem != null)
            {
                existingItem.Description = item.Description;
                existingItem.DueDate = item.DueDate;
                existingItem.IsDone = item.IsDone;
                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ToDoItem>> GetAllAsync()
        {
            return await context.ToDoItems.ToListAsync();
        }
    }
}
