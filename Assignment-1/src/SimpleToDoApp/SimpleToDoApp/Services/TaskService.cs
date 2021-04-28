using System;
using System.Linq;

using SimpleToDoApp.Models;

namespace SimpleToDoApp.Services
{
    public class TaskService
    {
        public Task CreateTask(string title, string description, ToDo toDo, User user) 
        {
            int newUniqueId = toDo.Tasks.Count + 1;
            DateTime now = DateTime.Now;

            Task task = new Task(newUniqueId, now, user.Id, now, user.Id, title, description, false, toDo.Id);

            return task;
        }

        public Task GetTaskById(ToDo toDo, int taskId)
        {
            Task task = toDo.Tasks.FirstOrDefault(t => t.Id == taskId);

            return task;
        }
    }
}
