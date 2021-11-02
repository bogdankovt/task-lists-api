using System;
using System.ComponentModel.DataAnnotations;

namespace task_lists_api.task_lists.Entities
{
    public class TaskEntity
    {   
        [Key]
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public bool IsDone { get; set; }
        public DateTime? DueDate { get; set; }

        public int ListId { get; set; }
        public TaskListEntity List { get; set; }
    }
}