using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using task_lists_api.task_lists.DTO;
using task_lists_api.task_lists.Entities;

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

        //get all tasks for list
        [HttpGet]
        public ActionResult<List<TaskDTO>> GetTasksForList(int listId, bool all)
        {    
            return 
                (all ? service.GetAllTaskForList(listId) : service.GetOpenTasksForList(listId))
                .Select(TaskDTO.FromEntity).ToList(); 
        }


        //create Task for Lists
        [HttpPost]
        public ActionResult<TaskDTO> CreateTaskForList(int listId, TaskEntity task)
        {   
            var createdItem = TaskDTO.FromEntity(service.CreateTaskForList(listId, task));
            return Created($"/tasks/{createdItem.TaskId}", createdItem);
        }




        [HttpPut("{id}")] //change Task
        public ActionResult<TaskDTO> replaceTask(TaskEntity task) 
        {
            return TaskDTO.FromEntity(service.ReplaceTask(task));
        }

        [HttpDelete("{id}")] //remove list id
        public ActionResult deleteTask(int id)
        {   
            var FindTaskOrNull = service.FindTask(id);
            if(FindTaskOrNull != null) {
                service.DeleteTask(FindTaskOrNull);
                return NoContent();
            }else {
                return NotFound();
            }
        }
    }
}