using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using task_lists_api.task_lists.DTO;
using task_lists_api.task_lists.Entities;

namespace task_lists_api.task_lists   //ListTasksController
{
    [Route("/lists/{listId}/tasks")]
    [ApiController]
    public class ListTasksController : ControllerBase
    {
        private TaskListService service;

        public ListTasksController(TaskListService service)
        {
            this.service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TaskDTO>> GetTasksForList(int listId, bool all)
        {    
            return all ? service.GetAllTaskForList(listId) : service.GetOpenTasksForList(listId); 
        }

        [HttpPost]
        public ActionResult<TaskDTO> CreateTaskForList(int listId, TaskEntity task)
        {   
            return service.CreateTaskForList(listId, task);
        }

        // [HttpPut("{id}")]
        // public ActionResult<Task> PutTaskInList(int listId, int id, Task task)
        // {
        //     return service.ReplaceTask(listId, id, task);
        // }

        // [HttpPatch("{id}")]
        // public ActionResult<Task> PatchTask(int listId,int id, [FromBody] JsonPatchDocument<Task> patchItem)
        // {   
        //     var patchedItem = service.GetAll()[listId].subTasks[id];
        //     patchItem.ApplyTo(patchedItem, ModelState);
        //     return patchedItem;
        // }

        // [HttpDelete("{id}")]
        // public ActionResult<Task> DeleteTaskFromList(int listId, int id)
        // {
        //     return service.DeleteTaskFromList(listId, id);
        // }
    }
}