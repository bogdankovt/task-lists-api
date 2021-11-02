using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using task_lists_api.task_lists.Entities;

namespace task_lists_api.task_lists
{
    [Route("/lists")]  //ListsController
    [ApiController]
    public class ListsController : ControllerBase
    {   
        private TaskListService service;
        public ListsController(TaskListService service)
        {
            this.service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TaskListEntity>> GetAllLists()
        {
            return service.GetAllLists();
        }

        [HttpGet("{id}")]
        public ActionResult<TaskListEntity> GetListById(int id)
        {
            return service.GetListById(id);
        }

        [HttpPost]
        public ActionResult<TaskListEntity> CreateList(TaskListEntity item)
        {   
            return service.CreateNewList(item);
        }

        // [HttpPut]
        // public ActionResult<TaskListEntity> ReplaceList(TaskListEntity item)
        // {
        //     return service.Replace(item);
        // }

        // [HttpPatch("{id}")]
        // public ActionResult<TaskList> PatchTodoItem(int id, [FromBody] JsonPatchDocument<TaskList> patchItem)
        // {   
        //     patchItem.ApplyTo(service.GetAll()[id], ModelState);
        //     return service.GetAll()[id];
        // }

        // [HttpDelete("{id}")]
        // public ActionResult<TaskListEntity> DeleteList(int id)
        // {
        //     return service.Delete(id);
        // }
    }
}