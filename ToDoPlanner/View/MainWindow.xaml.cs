using System;
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
            var taskModel = new TaskModel
            {
                // Input from User
                Title = "Task Example",
                Description = "An exemplary task to get the idea.",
                PriorityNum = (int) TaskModel.Priority.High,
                Deadline = DateTime.Parse("09.12.2019"),
                // DeadlineString = Deadline.ToString("dd.MM.yyyy"),
                StartDate = DateTime.Parse("23.01.2020"),
                Category = "C# Project",
                Effort = 50,
                Progress = 10,
                
                // Generated Input from System
                Created = DateTime.Today,
                Changed = DateTime.Today
            };

            DataContext = taskModel;
            InitializeComponent();
        }
    }
}