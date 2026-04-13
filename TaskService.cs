using System;
using System.Collections.Generic;
using SmartProductivityManager.Data;
using SmartProductivityManager.Events;
using SmartProductivityManager.Models;

namespace SmartProductivityManager.Services
{
    public class TaskService
    {
        private readonly TaskRepository _repo;

        public delegate void TaskEventHandler(object sender, TaskEventArgs e);

        public event TaskEventHandler? TaskAdded;
        public event TaskEventHandler? TaskCompleted;
        public event TaskEventHandler? TaskDeleted;
        public event TaskEventHandler? TaskOverdue;

        public TaskService()
        {
            _repo = new TaskRepository();
        }

        public List<TaskItem> GetAllTasks() => _repo.GetAll();
        public List<TaskItem> GetByStatus(Status status) => _repo.GetByStatus(status);
        public List<TaskItem> GetOverdueTasks() => _repo.GetOverdue();
        public List<TaskItem> Search(string keyword) => _repo.Search(keyword);

        public void AddTask(TaskItem task)
        {
            var daysLeft = (task.DueDate - DateTime.Now).TotalDays;

            if (daysLeft <= 1)
                task.Priority = Priority.High;
            else if (daysLeft <= 3)
                task.Priority = Priority.Medium;
            else
                task.Priority = Priority.Low;

            _repo.Add(task);
            TaskAdded?.Invoke(this, new TaskEventArgs(task,
                $"Task '{task.Title}' was added."));
        }

        public void CompleteTask(int id)
        {
            var task = _repo.GetById(id);
            if (task != null)
            {
                task.Status = Status.Completed;
                _repo.Update(task);
                TaskCompleted?.Invoke(this, new TaskEventArgs(task,
                    $"Task '{task.Title}' marked complete!"));
            }
        }

        public void DeleteTask(int id)
        {
            var task = _repo.GetById(id);
            if (task != null)
            {
                _repo.Delete(id);
                TaskDeleted?.Invoke(this, new TaskEventArgs(task,
                    $"Task '{task.Title}' was deleted."));
            }
        }

        public void CheckOverdueTasks()
        {
            var overdue = _repo.GetOverdue();
            foreach (var task in overdue)
            {
                TaskOverdue?.Invoke(this, new TaskEventArgs(task,
                    $"OVERDUE: '{task.Title}' was due on {task.DueDate:dd-MM-yyyy}"));
            }
        }

        public void UpdateTask(TaskItem task) => _repo.Update(task);
    }
}