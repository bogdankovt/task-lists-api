using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace task_lists_api.task_lists.Entities
{
    public class TaskListEntity
    {   
        [Key]
        public int ListId { get; set; }
        public string Title { get; set; }
        
        public List<TaskEntity> Tasks { get; set; }

    }
}