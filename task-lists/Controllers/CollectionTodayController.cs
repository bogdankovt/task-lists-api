using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using task_lists_api.task_lists;
using task_lists_api.task_lists.DTO;

namespace task_lists.Controllers
{
    [Route("/collection/today")]
    [ApiController]
    public class CollectionTodayController : ControllerBase
    {
        private TaskListService service;

        public CollectionTodayController(TaskListService service)
        {
            this.service = service;
        }

        [HttpGet]
        public ActionResult<List<TaskCollectioTodayDTO>> GetCollectionToday() => service.createCollectionToday();
        
    }
}