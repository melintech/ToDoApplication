﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<ToDoItem> ToDoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("uuid-ossp");
            modelBuilder.Entity<ToDoItem>(
                entity =>
                {
                    entity.ToTable("to_do_items");
                    entity.HasKey(e => e.Id);
                    entity.Property(e => e.Id)
                    .HasColumnType("uuid")
                    .HasDefaultValueSql("uuid_generate_v4()")
                    .ValueGeneratedOnAdd();
                    entity.Property(e => e.Description).IsRequired().HasMaxLength(200);
                    entity.Property(e => e.IsDone).HasDefaultValue(false);
                }
                
            );
        }
    }
}
