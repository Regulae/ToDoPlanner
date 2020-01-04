using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ToDoPlanner.Annotations;
using ToDoPlanner.Command;
using ToDoPlanner.Model;
using ToDoPlanner.UserControls;

namespace ToDoPlanner.ViewModel
{
    public class TaskViewModel : ViewModelBase
    {
        #region Properties

        /// <summary>
        /// The Task which is shown on the view
        /// </summary>
        private ToDoTask task;
        public ToDoTask Task {
            get => task;
            set => SetProperty(ref task, value);
        }

        private ToDoTask tempTask;      // The Task which was opened
        private TaskListViewModel taskListViewModel;

        #endregion

        #region Commands

        /// <summary>
        /// Command for add task to tasklist
        /// </summary>
        public ICommand ApplyCommand { get; set; }

        /// <summary>
        /// Command for cancel changes of the task
        /// </summary>
        public ICommand CancelCommand { get; set; }

        #endregion


        #region Constructor

        public TaskViewModel(TaskListViewModel tasklist)
        {
            taskListViewModel = tasklist;

            // Creat Commands
            ApplyCommand = new RelayCommand(Apply);
            CancelCommand = new RelayCommand(Cancel);

        }

        #endregion


        private void Apply()
        {
            taskListViewModel.AddTask(Task);
        }

        private void Cancel()
        {
            Task = tempTask;
        }

    }
}
