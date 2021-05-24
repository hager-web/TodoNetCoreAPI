using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoAPI.Models
{
    public class TodoContext : DbContext
    {
        public DbSet<Todo> Todos { get; set; }
        public DbSet<Category> Categories { get; set; }

        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Todo>()
                .HasOne<Category>(t => t.category)
                .WithMany(c => c.Todos)
                .HasForeignKey(t=>t.CategoryID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Category>()
               .HasData(new Category {CategoryID=1,Name="category1" });

            modelBuilder.Entity<Todo>()
                .HasData(
                    new Todo { TodoID = 1, Title = "todo1", Completed=true,CategoryID=1 },
                    new Todo { TodoID = 2, Title = "todo2", Completed = true, CategoryID = 1 },
                    new Todo { TodoID = 3, Title = "todo3", Completed = true, CategoryID = 1 });
            base.OnModelCreating(modelBuilder);

            /*Restrict: Prevents Cascade delete.
               SetNull: The values of foreign key properties in the dependent entities will be set to null.*/
        }
    }
}
