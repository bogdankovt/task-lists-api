using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using task_lists_api.task_lists.Contexts;
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
        internal List<TaskEntity> GetTasksForList(int listId)
        {
            return GetListById(listId).Tasks;
        }

        internal TaskEntity CreateTaskForList(int listId, TaskEntity task)
        {
            GetListById(listId).Tasks.Add(task);
            TaskListContext.SaveChanges();
            return task;
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
        internal int getCountTasksToday()
        {
            return TaskListContext.Tasks.Where(t => t.DueDate.Equals(DateTime.Today)).Count();
        }

        internal Dictionary<string, int> getCountNotDoneTasksByList()
        {
            return TaskListContext.Lists
            .Include(l => l.Tasks)
            .ToDictionary(l => l.Title + l.ListId, l => l.Tasks.Count == 0 ? -1 : l.Tasks.Where(t => t.IsDone.Equals(false)).Count());
        }
    }
}