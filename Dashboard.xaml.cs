using System;
using System.Linq;
using System.Windows.Controls;
using SmartProductivityManager.Models;

namespace SmartProductivityManager.UI
{
    public partial class Dashboard : Page
    {
        public Dashboard()
        {
            InitializeComponent();

            try
            {
                LoadDashboard();
            }
            catch { }
        }

        private void LoadDashboard()
        {
            var service = MainWindow.TaskService;
            var all     = service.GetAllTasks();

            DateText.Text       = DateTime.Now.ToString("dddd, dd MMMM yyyy");
            TotalCount.Text     = all.Count.ToString();
            CompletedCount.Text = all.Count(t => t.Status == Status.Completed).ToString();
            PendingCount.Text   = all.Count(t => t.Status == Status.Pending).ToString();
            OverdueCount.Text   = all.Count(t => t.IsOverdue).ToString();

            int overdue = all.Count(t => t.IsOverdue);
            int high    = all.Count(t => t.Priority == Priority.High
                                      && t.Status != Status.Completed);
            int total   = all.Count;
            int done    = all.Count(t => t.Status == Status.Completed);
            double rate = total > 0 ? Math.Round((double)done / total * 100) : 0;

            if (overdue > 0)
                SuggestionText.Text =
                    $"You have {overdue} overdue task(s). Focus on these first!";
            else if (high > 2)
                SuggestionText.Text =
                    $"{high} high priority tasks are pending. Complete them today.";
            else if (rate >= 80)
                SuggestionText.Text =
                    $"Excellent! {rate}% completion rate. You're crushing it!";
            else
                SuggestionText.Text =
                    "You're on track. Keep completing your tasks consistently.";

            RecentTasksList.ItemsSource = all
                .OrderByDescending(t => t.CreatedAt)
                .Take(6)
                .Select(t => new {
                    t.Title,
                    t.Category,
                    t.DueDate,
                    t.Priority,
                    PriorityColor = t.Priority == Priority.High   ? "#F87171" :
                                    t.Priority == Priority.Medium ? "#FCD34D" : "#34D399",
                    PriorityBg    = t.Priority == Priority.High   ? "#EF444418" :
                                    t.Priority == Priority.Medium ? "#F59E0B18" : "#10B98118"
                }).ToList();
        }
    }
}