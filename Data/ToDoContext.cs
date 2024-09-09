using Microsoft.EntityFrameworkCore;
using MyToDoAPI.Models;

namespace MyToDoAPI.Data
{
    public class ToDoContext : DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext> options)
            : base(options)
        {
        }

        public DbSet<ToDoItem>? ToDoItems { get; set; }
    }
}