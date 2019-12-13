using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
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
        [XmlElement(DataType = "date")] public DateTime Deadline { get; set; } = DateTime.Today;
        [XmlElement(DataType = "date")] public DateTime StartDate { get; set; } = DateTime.Today;
        private string _category;

        public string Category
        {
            get => _category;
            set => SetProperty(ref _category, value);
        }

        [XmlElement(DataType = "date")] public DateTime Created { get; set; } = DateTime.Today;
        [XmlElement(DataType = "date")] public DateTime Changed { get; set; } = DateTime.Today;
        public int Effort { get; set; }
        public int Progress { get; set; } // Implement as progressbar in the MainWindow.xaml
    }
}