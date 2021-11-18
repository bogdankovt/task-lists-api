using System;
using task_lists_api.task_lists.Entities;

namespace task_lists_api.task_lists.DTO
{
    public class TaskDTO
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public bool IsDone { get; set; }
        public DateTime? DueDate { get; set; }


        public static TaskDTO FromEntity(TaskEntity task) {
            
            return new TaskDTO(){TaskId = task.TaskId, Title = task.Title, Desc = task.Desc, IsDone = task.IsDone, DueDate = task.DueDate};
        }
    }
}