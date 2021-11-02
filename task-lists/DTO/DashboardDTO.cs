using System.Collections.Generic;

namespace task_lists_api.task_lists.DTO
{
    public class DashboardDTO
    {
        public int TasksForToday { get; set; }
        public Dictionary<string, int> NotDoneTasks {get; set;}
    }
}