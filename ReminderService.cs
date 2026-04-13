using System;
using System.Collections.Generic;
using System.Timers;
using SmartProductivityManager.Models;
using Timer = System.Timers.Timer;

namespace SmartProductivityManager.Services
{
    public class ReminderService
    {
        private readonly Timer _timer;
        private readonly TaskService _taskService;

        public delegate void ReminderHandler(string message);
        public event ReminderHandler? ReminderFired;

        public ReminderService(TaskService taskService)
        {
            _taskService = taskService;
            _timer = new Timer(60000);
            _timer.Elapsed += CheckReminders;
            _timer.AutoReset = true;
        }

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
            _timer.Dispose();
        }

        private void CheckReminders(object? sender, ElapsedEventArgs e)
        {
            try
            {
                _taskService.CheckOverdueTasks();

                List<TaskItem> all = _taskService.GetAllTasks();

                foreach (var task in all)
                {
                    if (task.Status == Status.Completed) continue;

                    double hoursLeft = (task.DueDate - DateTime.Now).TotalHours;

                    if (hoursLeft <= 24 && hoursLeft > 0)
                    {
                        ReminderFired?.Invoke(
                            $"Reminder: '{task.Title}' is due in less than 24 hours!");
                    }
                }
            }
            catch
            {
                // silently ignore background thread errors
            }
        }
    }
}