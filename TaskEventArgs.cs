using System;
using SmartProductivityManager.Models;

namespace SmartProductivityManager.Events
{
    public class TaskEventArgs : EventArgs
    {
        public TaskItem Task { get; }
        public string Message { get; }

        public TaskEventArgs(TaskItem task, string message)
        {
            Task = task;
            Message = message;
        }
    }
}