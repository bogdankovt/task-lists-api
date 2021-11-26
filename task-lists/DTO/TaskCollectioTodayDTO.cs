using System;

namespace task_lists_api.task_lists.DTO
{
    public class TaskCollectioTodayDTO
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public bool IsDone { get; set; }
        public DateTime? DueDate { get; set; }
        public string ListTitle {get; set; }
        public int listId { get; set; }
    }
}