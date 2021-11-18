using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using task_lists_api.task_lists.Contexts;
using task_lists_api.task_lists.DTO;
using task_lists_api.task_lists.Entities;

namespace task_lists_api.task_lists
{
    public class TaskListService
    {
        private TaskListContext TaskListContext;

        public TaskListService(TaskListContext taskListContext)
        {
            TaskListContext = taskListContext;
        }



        //lists
        internal List<TaskListEntity> GetAllLists() {
            return TaskListContext.Lists.OrderBy(l => l.ListId).ToList();
        }


        internal TaskListEntity CreateNewList(TaskListEntity item) {
            TaskListContext.Lists.Add(item);
            TaskListContext.SaveChanges();
            return item;
        }

        internal TaskListEntity GetListById(int id)
        {
            return TaskListContext.Lists
                .Where(l => l.ListId == id)
                .Include(l => l.Tasks)
                .First();
        }


        //tasks
        internal List<TaskDTO> GetOpenTasksForList(int listId)
        {
            return GetListById(listId).Tasks
            .Where(t => t.IsDone.Equals(false))
            .Select(t => new TaskDTO(){TaskId = t.TaskId, Title = t.Title, Desc = t.Desc, IsDone = t.IsDone, DueDate = t.DueDate})
            .ToList();
        }

        internal List<TaskDTO> GetAllTaskForList(int listId)
        {
            return GetListById(listId).Tasks
            .Select(t => new TaskDTO(){TaskId = t.TaskId, Title = t.Title, Desc = t.Desc, IsDone = t.IsDone, DueDate = t.DueDate})
            .OrderBy(t => t.IsDone)
            .ToList();
        }

        internal TaskDTO CreateTaskForList(int listId, TaskEntity task)
        {
            GetListById(listId).Tasks.Add(task);
            TaskListContext.SaveChanges();
            return new TaskDTO(){TaskId = task.TaskId, Title = task.Title, Desc = task.Desc, IsDone = task.IsDone, DueDate = task.DueDate};
        }

        // internal TaskListEntity GetListByIdWithTasks(int id) {
        //     return TaskListContext.Lists                
        //         .Where(l => l.ListId == id)
        //         .Include(l => l.Tasks)
        //         .Single();
        // }
        // internal TaskListEntity GetListById(int id) {
        //     return TaskListContext.Lists.First(l => l.ListId == id);
        // }


        // internal TaskListEntity Replace(TaskListEntity item)
        // {
        //     TaskListContext.Lists.Update(item);
        //     TaskListContext.SaveChanges();
        //     return item;
        // }

        // internal TaskListEntity Delete(int id) {
        //     var removeItem = GetListById(id);
        //     TaskListContext.Lists.Remove(removeItem);
        //     TaskListContext.SaveChanges();
        //     return removeItem;
        // }

        

        // //tasks

        // internal List<TaskEntity> GetTasks(int id) {
        //     return GetListById(id).Tasks;
        // }

        // internal List<Task> GetAllTasks()
        // {
        //     return items.SelectMany(t => t.subTasks).ToList();
        // }

        // internal Task ReplaceTask(int listId, int id, Task item)
        // {
        //     items[listId].subTasks[id] = item;
        //     return item;
        // }

        // internal TaskEntity CreateTaskForList(int listId, TaskEntity task)
        // {   
        //     task.ListId = listId;
        //     task.List = GetListById(listId);
        //     TaskListContext.Tasks.Add(task);
        //     TaskListContext.SaveChanges();
        //     return task;
        // }

        // internal Task DeleteTaskFromList(int listId, int id) {
        //     var removeItem = items[listId].subTasks[id];
        //     items[listId].subTasks.RemoveAt(id);
        //     return removeItem;
        // }

        //dashboard

        internal ActionResult<DashboardDTO> createDashboard() => new DashboardDTO()
        {
            TasksForToday = getCountTasksToday(),
            lists = getCountNotDoneTasksByList()

        };
        internal int getCountTasksToday()
        {
            return TaskListContext.Tasks.Where(t => t.DueDate.Equals(DateTime.Today)).Count();
        }

        internal List<ListDashboardDTO> getCountNotDoneTasksByList()
        {   
            return TaskListContext.Lists
            .Include(l => l.Tasks)
            .Select(l => new ListDashboardDTO() { ListId = l.ListId, title = l.Title, countNotDoneTask = l.Tasks.Where(t => t.IsDone.Equals(false)).Count()})
            .OrderBy(l => l.ListId)
            .ToList();

                // return TaskListContext.Lists.Join(TaskListContext.Tasks,
                //      l => l.ListId,
                //      t => t.ListId,

                //     (l, t) => new ListDashboardDTO(){ListId = l.ListId, title = l.Title, countNotDoneTask = 1}
                // ).ToList();
        }



        //Collection Today
        internal List<TaskCollectioTodayDTO> createCollectionToday()
        {
            return TaskListContext.Tasks
            .Include(t => t.List)
            .Where(t => t.DueDate.Equals(DateTime.Today))
            .Select(t => new TaskCollectioTodayDTO(){TaskId = t.TaskId, Title = t.Title, Desc = t.Desc, IsDone = t.IsDone, DueDate = t.DueDate, ListTitle = t.List.Title})
            .OrderBy(t => t.IsDone)
            .ToList();
        }
    }
}