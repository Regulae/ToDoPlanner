using System;
using System.Windows;
using ToDoPlanner.ViewModel;

namespace ToDoPlanner.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void TasksViewControl_Loaded(object sender, RoutedEventArgs e)
        {
            var taskViewModelObject = new TaskModel();
            taskViewModelObject.LoadTasks();
            taskViewModelObject.SaveTasks();
            TasksViewControl.DataContext = taskViewModelObject;
        }
    }
}