///------------------------------------------------------------------------
/// Namespace:    ToDoPlanner.ViewModel
/// Class:        TaskViewModel
/// Description:  See class summary
/// Author:       Kevin Kessler & Regula Engelhardt
/// Copyright:    (c) Kevin Kessler & Regula Engelhardt
///------------------------------------------------------------------------

using System.ComponentModel;
using System.Windows.Input;
using ToDoPlanner.Command;
using ToDoPlanner.Model;

namespace ToDoPlanner.ViewModel
{
    /// <summary>
    /// This view model class is about the task.
    /// It handles the editing of a task and send the save command to the referenced TaskListViewModel.
    /// The direct editing of the task in the TaskList is prevented.
    /// An active command has to be given to either save the edited Task or cancel the changes.
    /// </summary>
    public class TaskViewModel : ViewModelBase
    {
        #region Properties
        
        private ToDoTask task;
        /// <summary>
        /// The Task which is shown on the view
        /// </summary>
        public ToDoTask Task {
            get => task;
            set
            {
                if (value != null)
                {
                    // Make deep copy, so you don't edit the source task directly
                    task = value.Clone();
                    // Store the reference of the source task, for save the changes later to the source
                    sourceTask = value;
                    task.PropertyChanged += taskChanged;
                }
                else
                {
                    task = value;
                }

                NotifyPropertyChanged();
                HasChanged = false;

            }
        }

        /// <summary>
        /// The task which was opened
        /// </summary>
        private ToDoTask sourceTask;
        /// <summary>
        /// The reference to the TaskListViewModel
        /// </summary>
        private TaskListViewModel taskListViewModel;
        
        
        private bool hasChanged;
        /// <summary>
        /// True if the task has been changed
        /// </summary>
        public bool HasChanged
        {
            get => hasChanged;
            set => SetProperty(ref hasChanged, value);
        }

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

        #region Methods

        /// <summary>
        /// Apply changes and save it to the database
        /// </summary>
        private void Apply()
        {
            sourceTask?.Copy(Task);
            taskListViewModel.AddTask(sourceTask);
            HasChanged = false;
        }

        /// <summary>
        /// Cancel all changes to the task
        /// </summary>
        private void Cancel()
        {
            Task.Copy(sourceTask);
            HasChanged = false;
        }

        /// <summary>
        /// Event to notice if the task has changed
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="e">The Arguments of the event</param>
        private void taskChanged(object sender, PropertyChangedEventArgs e)
        {
            HasChanged = true;
        }

        #endregion
    }
}
