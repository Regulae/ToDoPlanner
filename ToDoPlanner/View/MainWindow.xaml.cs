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

        TaskListViewModel taskViewModelObject = new TaskListViewModel();

        public MainWindow()
        {
            InitializeComponent();
            Closing += taskViewModelObject.Close;
            

        }

        private void TasksViewControl_Loaded(object sender, RoutedEventArgs e)
        {
            TaskListViewControl.DataContext = taskViewModelObject;
            TasksViewControl.DataContext = taskViewModelObject.TaskViewModelControl;
            taskViewModelObject.Initialize();
        }
        
    }
}