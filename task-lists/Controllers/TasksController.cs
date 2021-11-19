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
            return TaskDTO.FromEntity(service.CreateTaskForList(listId, task));
        }


        [HttpPut("{id}")] //remove list id
        public ActionResult<TaskDTO> replaceTask(TaskEntity task) 
        {
            return TaskDTO.FromEntity(service.replaceTask(task));
        }

        [HttpDelete("{id}")] //remove list id
        public ActionResult<TaskDTO> deleteTask(int id)
        {
            return TaskDTO.FromEntity(service.deleteTask(id));
        }
    }
}