using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using task_lists_api.task_lists;
using task_lists_api.task_lists.DTO;

namespace task_lists.Controllers
{
    [Route("/dashboard")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private TaskListService service;

        public DashboardController(TaskListService service)
        {
            this.service = service;
        }

        [HttpGet]
        public ActionResult<DashboardDTO> GetDashboard() => new DashboardDTO()
        {
            TasksForToday = service.getCountTasksToday(),
            NotDoneTasks = service.getCountNotDoneTasksByList()

        };
    }
}