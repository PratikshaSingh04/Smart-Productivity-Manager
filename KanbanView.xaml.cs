using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using SmartProductivityManager.Models;

namespace SmartProductivityManager.UI
{
    public partial class KanbanView : Page
    {
        public KanbanView()
        {
            InitializeComponent();
            LoadKanban();
        }

        private void LoadKanban()
        {
            try
            {
                var service  = MainWindow.TaskService;
                var pending  = service.GetByStatus(Status.Pending);
                var inProg   = service.GetByStatus(Status.InProgress);
                var completed = service.GetByStatus(Status.Completed);

                PendingCountText.Text    = pending.Count.ToString();
                InProgressCountText.Text = inProg.Count.ToString();
                CompletedCountText.Text  = completed.Count.ToString();

                PendingList.ItemsSource = pending.Select(t => new {
                    t.Id, t.Title, t.Category, t.DueDate, t.Priority,
                    PriorityColor = t.Priority == Priority.High   ? "#F87171" :
                                    t.Priority == Priority.Medium ? "#FCD34D" : "#34D399",
                    PriorityBg    = t.Priority == Priority.High   ? "#EF444425" :
                                    t.Priority == Priority.Medium ? "#F59E0B25" : "#10B98125"
                }).ToList();

                InProgressList.ItemsSource = inProg.Select(t => new {
                    t.Id, t.Title, t.Category, t.DueDate, t.Priority,
                    PriorityColor = t.Priority == Priority.High   ? "#F87171" :
                                    t.Priority == Priority.Medium ? "#FCD34D" : "#34D399",
                    PriorityBg    = t.Priority == Priority.High   ? "#EF444425" :
                                    t.Priority == Priority.Medium ? "#F59E0B25" : "#10B98125"
                }).ToList();

                CompletedList.ItemsSource = completed.Select(t => new {
                    t.Id, t.Title, t.Category
                }).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading Kanban: {ex.Message}");
            }
        }

        private void MoveToInProgress_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is int id)
            {
                var task = MainWindow.TaskService.GetAllTasks()
                                     .FirstOrDefault(t => t.Id == id);
                if (task != null)
                {
                    task.Status = Status.InProgress;
                    MainWindow.TaskService.UpdateTask(task);
                    LoadKanban();
                }
            }
        }

        private void MarkDone_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is int id)
            {
                MainWindow.TaskService.CompleteTask(id);
                LoadKanban();
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
            => LoadKanban();
    }
}