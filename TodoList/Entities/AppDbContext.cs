using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace TodoList.Entities
{
    public class AppDbContext : DbContext
    {
        public DbSet<TodoListEntity> TodoLists { get; set; } = null!;
        public DbSet<TodoItemEntity> TodoItems { get; set; } = null!;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<TodoListEntity>()
                .ToTable("todo_lists")
                .HasKey(x => x.Id);

            modelBuilder.Entity<TodoItemEntity>()
                .ToTable("todo_items")
                .HasKey(x => x.Id);


            modelBuilder.Entity<TodoListEntity>()
                .HasMany(x => x.TodoItems)
                .WithOne(x => x.TodoList)
                .HasForeignKey(x => x.TodoListId);

            modelBuilder.Entity<TodoListEntity>()
                .Property(x => x.Id)
                .UseIdentityColumn();

            modelBuilder.Entity<TodoItemEntity>()
                .Property(x => x.Id)
                .UseIdentityColumn();
        }
    }
}
