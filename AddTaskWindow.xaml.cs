using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using SmartProductivityManager.Models;

namespace SmartProductivityManager.UI
{
    public partial class AddTaskWindow : Page
    {
        public AddTaskWindow()
        {
            InitializeComponent();
            DueDatePicker.SelectedDate = DateTime.Now.AddDays(1);
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TitleBox.Text))
            {
                ResultMsg.Foreground = Brushes.Red;
                ResultMsg.Text = "⚠ Please enter a task title.";
                return;
            }

            if (DueDatePicker.SelectedDate == null)
            {
                ResultMsg.Foreground = Brushes.Red;
                ResultMsg.Text = "⚠ Please select a due date.";
                return;
            }

            var task = new TaskItem
            {
                Title       = TitleBox.Text.Trim(),
                Description = DescBox.Text.Trim(),
                Category    = (CategoryBox.SelectedItem as ComboBoxItem)?
                               .Content.ToString() ?? "General",
                DueDate     = DueDatePicker.SelectedDate.Value,
                Status      = Status.Pending,
                CreatedAt   = DateTime.Now
            };

            MainWindow.TaskService.AddTask(task);

            ResultMsg.Foreground = new SolidColorBrush(Color.FromRgb(52, 211, 153));
            ResultMsg.Text = $"✓ Task '{task.Title}' added successfully!";

            TitleBox.Text  = "";
            DescBox.Text   = "";
            CategoryBox.SelectedIndex  = 0;
            DueDatePicker.SelectedDate = DateTime.Now.AddDays(1);
        }
    }
}