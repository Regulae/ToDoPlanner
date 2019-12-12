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

        TaskModel taskViewModelObject = new TaskModel();

        public MainWindow()
        {
            InitializeComponent();
            Closing += taskViewModelObject.SaveTasks;

        }

        private void TasksViewControl_Loaded(object sender, RoutedEventArgs e)
        {
            taskViewModelObject.LoadTasks();
            TasksViewControl.DataContext = taskViewModelObject;
            
        }
        
    }
}