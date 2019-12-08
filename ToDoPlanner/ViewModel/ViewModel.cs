using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ToDoPlanner.Annotations;

namespace ToDoPlanner.ViewModel
{
    public class TaskModel : ViewModelBase
    {
        private string _title;

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private string _description;

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public enum Priority
        {
            Low,
            Medium,
            High
        }

        public int PriorityNum { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime StartDate { get; set; }
        private string _category;

        public string Category
        {
            get => _category;
            set => SetProperty(ref _category, value);
        }

        public enum Status
        {
            None,
            Open,
            Planned,
            Ongoing,
            Done
        }

        public DateTime Created { get; set; }
        public DateTime Changed { get; set; }
        public int Effort { get; set; }
        public int Progress { get; set; } // Implement as progressbar in the MainWindow.xaml
    }
}