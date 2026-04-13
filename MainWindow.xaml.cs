using System;
using System.Windows;
using SmartProductivityManager.Services;
using SmartProductivityManager.UI;

namespace SmartProductivityManager
{
    public partial class MainWindow : Window
    {
        public static TaskService TaskService = new TaskService();
        public static NotificationService? NotificationService;
        public static ReminderService?     ReminderService;
        public static ReportService?       ReportService;

        public MainWindow()
        {
            InitializeComponent();

            try
            {
                NotificationService = new NotificationService(TaskService);
                ReminderService     = new ReminderService(TaskService);
                ReportService       = new ReportService(TaskService);

                ReminderService.ReminderFired += msg =>
                {
                    try
                    {
                        Dispatcher.Invoke(() =>
                            MessageBox.Show(msg, "Reminder",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information));
                    }
                    catch { }
                };

                ReminderService.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Startup error: {ex.Message}", "Error");
            }

            MainFrame.Navigate(new Dashboard());
        }

        private void ShowDashboard(object sender, RoutedEventArgs e)
        {
            try { MainFrame.Navigate(new Dashboard()); }
            catch { }
        }

        private void ShowTaskList(object sender, RoutedEventArgs e)
        {
            try { MainFrame.Navigate(new TaskListView()); }
            catch { }
        }

        private void ShowAddTask(object sender, RoutedEventArgs e)
        {
            try { MainFrame.Navigate(new AddTaskWindow()); }
            catch { }
        }

        private void ShowKanban(object sender, RoutedEventArgs e)
        {
            try { MainFrame.Navigate(new KanbanView()); }
            catch { }
        }

        private void ShowReports(object sender, RoutedEventArgs e)
        {
            try { MainFrame.Navigate(new ReportsView()); }
            catch { }
        }

        protected override void OnClosed(EventArgs e)
        {
            try
            {
                ReminderService?.Stop();
            }
            catch { }
            base.OnClosed(e);
        }
    }
}