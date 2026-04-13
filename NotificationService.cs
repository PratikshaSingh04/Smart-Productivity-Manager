using System;
using SmartProductivityManager.Events;

namespace SmartProductivityManager.Services
{
    public class NotificationService
    {
        private readonly TaskService _taskService;

        public NotificationService(TaskService taskService)
        {
            _taskService = taskService;

            _taskService.TaskAdded     += OnTaskAdded;
            _taskService.TaskCompleted += OnTaskCompleted;
            _taskService.TaskDeleted   += OnTaskDeleted;
            _taskService.TaskOverdue   += OnTaskOverdue;
        }

        private void OnTaskAdded(object sender, TaskEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"[+] {e.Message}");
            Console.ResetColor();
        }

        private void OnTaskCompleted(object sender, TaskEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[✓] {e.Message}");
            Console.ResetColor();
        }

        private void OnTaskDeleted(object sender, TaskEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[-] {e.Message}");
            Console.ResetColor();
        }

        private void OnTaskOverdue(object sender, TaskEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[!] {e.Message}");
            Console.ResetColor();
        }
    }
}