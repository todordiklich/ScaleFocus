using System;
using System.Linq;
using System.Collections.Generic;

using SimpleToDoApp.Data;
using SimpleToDoApp.Models;

namespace SimpleToDoApp.Services
{
    public class ToDoService
    {
        private const string StoreFileName = "ToDos.json";

        private readonly FileDatabase _storage;

        private readonly List<ToDo> _applicationToDos = new List<ToDo>();

        public ToDoService()
        {
            _storage = new FileDatabase();
            List<ToDo> toDosFromFile = _storage.Read<List<ToDo>>(StoreFileName);

            if (toDosFromFile != null)
            {
                _applicationToDos = toDosFromFile;
            }
        }

        public bool CreateToDoList(string title, User user)
        {
            if (_applicationToDos.Any(t => t.Title == title))
            {
                return false;
            }

            int newUniqueId = _applicationToDos.Count + 1;

            DateTime now = DateTime.Now;

            var toDo = new ToDo(newUniqueId, now, user.Id, now, user.Id, title);

            _applicationToDos.Add(toDo);

            SaveToFile();

            return true;
        }

        private void SaveToFile()
        {
            _storage.Write(StoreFileName, _applicationToDos);
        }
        public void SaveChanges()
        {
            SaveToFile();
        }

        public ToDo GetToDoById(int toDoId)
        {
            return _applicationToDos.FirstOrDefault(t => t.Id == toDoId);
        }

        public void DeleteToDoById(int toDoId)
        {
            ToDo toDo = GetToDoById(toDoId);

            _applicationToDos.Remove(toDo);
            SaveToFile();
        }

        public IReadOnlyCollection<ToDo> ListAllUserToDoLists(int userId)
        {
            return _applicationToDos
                .Where(t => t.CreatorId == userId || t.SharedWithUsersIds.Contains(userId))
                .ToList()
                .AsReadOnly();
        }

        public void ShareToDoList(int toDoId, int userId)
        {
            ToDo toDo = GetToDoById(toDoId);
            toDo.SharedWithUsersIds.Add(userId);
        }

        public bool IsToDoWithIdContained(int toDoId)
        {
            if (_applicationToDos.Any(t => t.Id == toDoId))
            {
                return true;
            }

            return false;
        }
    }
}
