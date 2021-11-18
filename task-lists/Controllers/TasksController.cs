using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using task_lists_api.task_lists.DTO;

namespace task_lists_api.task_lists   
{
    [Route("/tasks")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private TaskListService service;

        public TasksController(TaskListService service)
        {
            this.service = service;
        }

        // [HttpGet]
        // public ActionResult<IEnumerable<Task>> GetTasksForList(int? listId)
        // {
        //         if(listId.HasValue) {
        //             return service.GetTasks(listId.Value);
        //         }else {
        //             return service.GetAllTasks();
        //         }
        // }

        // [HttpGet("{id}")] //remove list id
        // public ActionResult<Task> GetTaskById(int listId, int id)
        // {
        //     return service.GetTasks(listId)[id];
        // }

        // [HttpPost]  //remove list id
        // public ActionResult<Task> CreateTaskForList(int listId, Task task)
        // {   
        //     service.CreateTaskForList(listId, task);
        //     return task;
        // }

        // [HttpPut("{id}")] //remove list id
        // public ActionResult<Task> PutTaskInList(int listId, int id, Task task)
        // {
        //     return service.ReplaceTask(listId, id, task);
        // }

        // [HttpPatch("{id}")] //remove list id
        // public ActionResult<Task> PatchTask(int listId,int id, [FromBody] JsonPatchDocument<Task> patchItem)
        // {   
        //     var patchedItem = service.GetAll()[listId].subTasks[id];
        //     patchItem.ApplyTo(patchedItem, ModelState);
        //     return patchedItem;
        // }


        //tasks edit
        [HttpPut("{id}/edit")] //remove list id
        public ActionResult<TaskDTO> replaceTask(TaskDTO task) 
        {
            return service.replaceTask(task);
        }

        [HttpDelete("{id}")] //remove list id
        public ActionResult<TaskDTO> deleteTask(int id)
        {
            return service.deleteTask(id);
        }
    }
}