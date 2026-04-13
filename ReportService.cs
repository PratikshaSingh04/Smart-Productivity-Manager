using System;
using System.Linq;
using SmartProductivityManager.Models;

namespace SmartProductivityManager.Services
{
    public class ReportService
    {
        private readonly TaskService _taskService;

        public ReportService(TaskService taskService)
        {
            _taskService = taskService;
        }

        public void PrintSummary()
        {
            var all = _taskService.GetAllTasks();

            int total      = all.Count;
            int completed  = all.Count(t => t.Status == Status.Completed);
            int pending    = all.Count(t => t.Status == Status.Pending);
            int inProgress = all.Count(t => t.Status == Status.InProgress);
            int overdue    = all.Count(t => t.IsOverdue);
            int high       = all.Count(t => t.Priority == Priority.High);

            double rate = total > 0
                ? Math.Round((double)completed / total * 100, 1)
                : 0;

            Console.WriteLine("\n======== PRODUCTIVITY REPORT ========");
            Console.WriteLine($"  Total Tasks     : {total}");
            Console.WriteLine($"  Completed       : {completed}");
            Console.WriteLine($"  Pending         : {pending}");
            Console.WriteLine($"  In Progress     : {inProgress}");
            Console.WriteLine($"  Overdue         : {overdue}");
            Console.WriteLine($"  High Priority   : {high}");
            Console.WriteLine($"  Completion Rate : {rate}%");

            Console.WriteLine("\n--- Smart Suggestions ---");
            if (overdue > 0)
                Console.WriteLine($"  ! You have {overdue} overdue task(s). Focus on these first!");
            if (high > 2)
                Console.WriteLine($"  ! Too many high priority tasks. Try to reschedule some.");
            if (rate >= 80)
                Console.WriteLine("  Great job! Your completion rate is excellent.");

            Console.WriteLine("\n--- Tasks by Category ---");
            var byCategory = all
                .GroupBy(t => t.Category)
                .Select(g => new { Category = g.Key, Count = g.Count() });

            foreach (var group in byCategory)
                Console.WriteLine($"  {group.Category} : {group.Count} task(s)");

            Console.WriteLine("=====================================\n");
        }
    }
}