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
        
        //global
        internal TaskEntity FindTask(int id) {
            return TaskListContext.Tasks.Find(id);
        }

        internal TaskListEntity FindList(int id) {
            return TaskListContext.Lists.Find(id);
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

        internal TaskListEntity GetListById(int id){
            return TaskListContext.Lists
                    .Include(l => l.Tasks)
                    .FirstOrDefault(l => l.ListId == id);
        }

        internal void DeleteList(TaskListEntity list) {
            TaskListContext.Lists.Remove(list);
            TaskListContext.SaveChanges();
        }


            

        


        //tasks 
        //get
        internal IEnumerable<TaskEntity> GetOpenTasksForList(int listId)
        {
            return GetListById(listId).Tasks
            .Where(t => t.IsDone.Equals(false));
        }

        internal IEnumerable<TaskEntity> GetAllTaskForList(int listId)
        {
            return GetListById(listId).Tasks.OrderBy(t => t.IsDone);
        }

        //create
        internal TaskEntity CreateTaskForList(int listId, TaskEntity task)
        {   
            GetListById(listId).Tasks.Add(task);
            TaskListContext.SaveChanges();
            return task;
        }

        internal void DeleteTask(TaskEntity task)
        {
            TaskListContext.Tasks.Remove(task);
            TaskListContext.SaveChanges();
        }


        internal TaskEntity ReplaceTask(TaskEntity task) {

            var replacedTask = TaskListContext.Tasks.Find(task.TaskId);

            replacedTask.Title = task.Title;
            replacedTask.Desc = task.Desc;
            replacedTask.IsDone = task.IsDone;
            replacedTask.DueDate = task.DueDate;

            TaskListContext.SaveChanges();
            return replacedTask;
        }


        //dashboard
        internal DashboardDTO createDashboard() => new DashboardDTO()
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