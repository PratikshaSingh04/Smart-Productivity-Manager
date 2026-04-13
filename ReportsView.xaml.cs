using System;
using System.Linq;
using System.Windows.Controls;
using SmartProductivityManager.Models;

namespace SmartProductivityManager.UI
{
    public partial class ReportsView : Page
    {
        public ReportsView()
        {
            InitializeComponent();
            LoadReports();
        }

        private void LoadReports()
        {
            var all = MainWindow.TaskService.GetAllTasks();
            int total     = all.Count;
            int completed = all.Count(t => t.Status == Status.Completed);
            int high      = all.Count(t => t.Priority == Priority.High);
            int overdue   = all.Count(t => t.IsOverdue);
            double rate   = total > 0
                ? Math.Round((double)completed / total * 100, 1) : 0;

            RateText.Text   = $"{rate}%";
            HighText.Text   = high.ToString();
            OverdueText.Text = overdue.ToString();

            CategoryList.ItemsSource = all
                .GroupBy(t => t.Category)
                .Select(g => new { Category = g.Key, Count = g.Count() })
                .ToList();
        }
    }
}