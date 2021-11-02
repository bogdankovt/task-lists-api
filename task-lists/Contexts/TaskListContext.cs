using Microsoft.EntityFrameworkCore;
using task_lists_api.task_lists.Entities;

namespace task_lists_api.task_lists.Contexts
{
    public class TaskListContext : DbContext
    {
        public DbSet<TaskListEntity> Lists {get; set;}
        public DbSet<TaskEntity> Tasks { get; set; }

        public TaskListContext(DbContextOptions options) : base(options)
        {
        }
    }
}