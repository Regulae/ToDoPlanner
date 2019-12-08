using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoPlanner.ViewModel;

namespace ToDoPlanner.Model
{
    public enum Priority
    {
        Low,
        Medium,
        High
    }

    public enum Status
    {
        None,
        Open,
        Planned,
        Ongoing,
        Done
    }

    public class ToDoTask : ModelBase
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

        public int PriorityNum { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime StartDate { get; set; }
        private string _category;

        public string Category
        {
            get => _category;
            set => SetProperty(ref _category, value);
        }

        public DateTime Created { get; set; }
        public DateTime Changed { get; set; }
        public int Effort { get; set; }
        public int Progress { get; set; } // Implement as progressbar in the MainWindow.xaml

    }
}