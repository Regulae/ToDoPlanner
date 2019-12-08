using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ToDoPlanner.Annotations;
using ToDoPlanner.Command;

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
        
        // @TODO Regula 08.12.19: Implement Commands for the EditView
        /*        // Commands
        private readonly DelegateCommand _changeTitleCommand;
        public ICommand ChangeTitleCommand => _changeTitleCommand;

        public TaskModel()
        {
            _changeTitleCommand = new DelegateCommand(OnChangeTitle);
        }

        private void OnChangeTitle(object commandParameter)
        {
            Deadline = DateTime.Parse("12.12.2012");
        }*/
    }
}