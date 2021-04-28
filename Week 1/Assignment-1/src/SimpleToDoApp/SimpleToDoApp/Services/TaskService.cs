using System;
using System.Linq;

using SimpleToDoApp.Models;

namespace SimpleToDoApp.Services
{
    public class TaskService
    {
        public Task CreateTask(string title, string description, ToDo toDo, User user) 
        {
            int newUniqueId = GenerateTaskId(toDo);
            DateTime now = DateTime.Now;

            Task task = new Task(newUniqueId, now, user.Id, now, user.Id, title, description, false, toDo.Id);

            return task;
        }

        private int GenerateTaskId(ToDo toDo)
        {
            if (toDo.Tasks.Any())
            {
                return toDo.Tasks.Max(t => t.Id) + 1;
            }

            return 1;
        }

        public Task GetTaskById(ToDo toDo, int taskId)
        {
            Task task = toDo.Tasks.FirstOrDefault(t => t.Id == taskId);

            return task;
        }
    }
}
