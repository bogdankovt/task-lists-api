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
            var createdList = service.CreateNewList(item);
            return Created($"/lists/{createdList.ListId}", createdList);
        }

        [HttpDelete("{id}")]
        public ActionResult<TaskListEntity> DeleteList(int id)
        {    
            var FindListOrNull = service.FindList(id);
            if(FindListOrNull != null) {
                service.DeleteList(FindListOrNull);
                return NoContent();
            }else return NotFound();                 
        }

    }
}