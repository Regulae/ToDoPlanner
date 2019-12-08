using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ToDoPlanner.Annotations;
using ToDoPlanner.Command;
using ToDoPlanner.Model;

namespace ToDoPlanner.ViewModel
{
    public class TaskModel : ViewModelBase
    {

        public ObservableCollection<ToDoTask> ToDoTasks { get; set; }

        public void LoadTasks()
        {
            ObservableCollection<ToDoTask> toDoTasks = new ObservableCollection<ToDoTask>();
            
            toDoTasks.Add (new ToDoTask(){
                // Input from User
                Title = "Task Example",
                Description = "An exemplary task to get the idea.",
                PriorityNum = (int)Priority.High,
                Deadline = DateTime.Parse("09.12.2019"),
                // DeadlineString = Deadline.ToString("dd.MM.yyyy"),
                StartDate = DateTime.Parse("23.01.2020"),
                Category = "C# Project",
                Effort = 50,
                Progress = 10,
                
                // Generated Input from System
                Created = DateTime.Today,
                Changed = DateTime.Today
            });

            ToDoTasks = toDoTasks;
        }
        
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