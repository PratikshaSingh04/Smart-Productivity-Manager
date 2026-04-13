using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using SmartProductivityManager.Models;

namespace SmartProductivityManager.UI
{
    public partial class TaskListView : Page
    {
        public TaskListView()
        {
            InitializeComponent();
            LoadTasks();
        }

        private void LoadTasks(string keyword = "", string filter = "All Status")
        {
            try
            {
                var tasks = string.IsNullOrWhiteSpace(keyword)
                    ? MainWindow.TaskService.GetAllTasks()
                    : MainWindow.TaskService.Search(keyword);

                if (filter == "Pending")
                    tasks = tasks.Where(t => t.Status == Status.Pending).ToList();
                else if (filter == "In Progress")
                    tasks = tasks.Where(t => t.Status == Status.InProgress).ToList();
                else if (filter == "Completed")
                    tasks = tasks.Where(t => t.Status == Status.Completed).ToList();

                TaskCountText.Text = $"{tasks.Count} task(s) found";

                TasksList.ItemsSource = tasks.Select(t => new {
                    t.Id,
                    t.Title,
                    t.Category,
                    t.DueDate,
                    t.Priority,
                    t.Status,
                    PriorityColor = t.Priority == Priority.High   ? "#F87171" :
                                    t.Priority == Priority.Medium ? "#FCD34D" : "#34D399",
                    PriorityBg    = t.Priority == Priority.High   ? "#EF444425" :
                                    t.Priority == Priority.Medium ? "#F59E0B25" : "#10B98125",
                    StatusColor   = t.Status == Status.Completed  ? "#34D399" :
                                    t.Status == Status.InProgress ? "#93C5FD"  : "#9CA3AF",
                    StatusBg      = t.Status == Status.Completed  ? "#10B98125" :
                                    t.Status == Status.InProgress ? "#3B82F625" : "#6B728025"
                }).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading tasks: {ex.Message}");
            }
        }

        private void MarkInProgress_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is int id)
            {
                var task = MainWindow.TaskService.GetAllTasks()
                                     .FirstOrDefault(t => t.Id == id);
                if (task != null)
                {
                    task.Status = Status.InProgress;
                    MainWindow.TaskService.UpdateTask(task);
                    LoadTasks(SearchBox.Text,
                        (FilterBox.SelectedItem as ComboBoxItem)?.Content.ToString()
                        ?? "All Status");
                }
            }
        }

        private void MarkComplete_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is int id)
            {
                MainWindow.TaskService.CompleteTask(id);
                LoadTasks(SearchBox.Text,
                    (FilterBox.SelectedItem as ComboBoxItem)?.Content.ToString()
                    ?? "All Status");
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is int id)
            {
                var result = MessageBox.Show(
                    "Are you sure you want to delete this task?",
                    "Confirm Delete",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    MainWindow.TaskService.DeleteTask(id);
                    LoadTasks(SearchBox.Text,
                        (FilterBox.SelectedItem as ComboBoxItem)?.Content.ToString()
                        ?? "All Status");
                }
            }
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
            => LoadTasks(SearchBox.Text,
                (FilterBox.SelectedItem as ComboBoxItem)?.Content.ToString()
                ?? "All Status");

        private void FilterBox_Changed(object sender, SelectionChangedEventArgs e)
            => LoadTasks(SearchBox.Text,
                (FilterBox.SelectedItem as ComboBoxItem)?.Content.ToString()
                ?? "All Status");

        private void Refresh_Click(object sender, RoutedEventArgs e)
            => LoadTasks();
    }
}