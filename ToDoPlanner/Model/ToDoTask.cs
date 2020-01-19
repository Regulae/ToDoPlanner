///------------------------------------------------------------------------
/// Namespace:    ToDoPlanner.Model
/// Class:        ToDoTask
/// Author:       Kevin Kessler & Regula Engelhardt
/// Copyright:    (c) Kevin Kessler & Regula Engelhardt
///------------------------------------------------------------------------

using System;
using Newtonsoft.Json;

namespace ToDoPlanner.Model
{
    /// <summary>
    /// Enum for priority levels of a task
    /// </summary>
    public enum Priority
    {
        Low,
        Medium,
        High
    }

    /// <summary>
    /// Enum for the status of a task
    /// </summary>
    public enum Status
    {
        None,
        Open,
        Planned,
        Ongoing,
        Done
    }

    /// <summary>
    /// The ToDoTask class. This class hold all information about a ToDoTask.
    /// </summary>
    public class ToDoTask : ModelBase
    {
        private int? _id;
        /// <summary>
        /// Unique ID in the database.
        /// </summary>
        [JsonProperty("id")]
        public int? Id
        {
            get => _id ?? null;
            set => SetProperty(ref _id, value);
        }
        
        private string _title;
        /// <summary>
        /// The title of the task
        /// </summary>
        [JsonProperty("title")]
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private string _description;
        /// <summary>
        /// The description of the task
        /// </summary>
        [JsonProperty("description")]
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        private int _priorityNum;
        /// <summary>
        /// The priority of a task
        /// </summary>
        [JsonProperty("priority")]
        public int PriorityNum
        {
            get => _priorityNum;
            set => SetProperty(ref _priorityNum, value);
        }

        private int _statusNum;
        /// <summary>
        /// The status of a task
        /// </summary>
        [JsonProperty("status")]
        public int StatusNum
        {
            get => _statusNum;
            set => SetProperty(ref _statusNum, value);
        }

        private DateTime _deadline = DateTime.Today;
        /// <summary>
        /// The deadline date of the task
        /// </summary>
        [JsonProperty("deadline")]
        public DateTime Deadline
        {
            get => _deadline;
            set => SetProperty(ref _deadline, value);
        }

        private DateTime _startDate = DateTime.Today;
        /// <summary>
        /// The date to start with the task
        /// </summary>
        [JsonProperty("start_date")]
        public DateTime StartDate
        {
            get => _startDate;
            set => SetProperty(ref _startDate, value);
        }

        private string _category;
        /// <summary>
        /// The category of a task
        /// </summary>
        [JsonProperty("category")]
        public string Category
        {
            get => _category;
            set => SetProperty(ref _category, value);
        }

        private DateTime _created = DateTime.Today;
        /// <summary>
        /// The creation date of a task
        /// </summary>
        [JsonProperty("created")]
        public DateTime Created
        {
            get => _created;
            set => SetProperty(ref _created, value);
        }

        private DateTime _changed = DateTime.Today;
        /// <summary>
        /// The date when the task has been last changed
        /// </summary>
        [JsonProperty("changed")]
        public DateTime Changed
        {
            get => _changed;
            set => SetProperty(ref _changed, value);
        }

        private int _effort;
        /// <summary>
        /// The effort of a task
        /// </summary>
        [JsonProperty("effort")]
        public int Effort
        {
            get => _effort;
            set => SetProperty(ref _effort, value);
        }

        private int _progress;
        /// <summary>
        /// The progress of a task
        /// </summary>
        [JsonProperty("progress")]
        public int Progress // Implement as progressbar in the MainWindow.xaml
        {
            get => _progress;
            set => SetProperty(ref _progress, value);
        }
        
        #region Helper methods

        /// <summary>
        /// Clone the actual task, so the reference is not the same anymore but the values are.
        /// </summary>
        /// <returns>A deep copy of the task</returns>
        public ToDoTask Clone()
        {
            return new ToDoTask()
            {
                Title = (this.Title == null ? null : String.Copy(this.Title)),
                Description = (this.Description == null ? null : String.Copy(this.Description)),
                PriorityNum = this.PriorityNum,
                Deadline = this.Deadline,
                StartDate = this.StartDate,
                Category = (this.Category == null ? null : String.Copy(this.Category)),
                Created = this.Created,
                Changed = this.Changed,
                Effort = this.Effort,
                Progress = this.Progress,
                StatusNum = this.StatusNum
            };
        }

        /// <summary>
        /// Copy all values and references
        /// </summary>
        /// <param name="task">The task of which the values has to be copied</param>
        public void Copy(ToDoTask task)
        {
            this.Title = task.Title;
            this.Description = task.Description;
            this.PriorityNum = task.PriorityNum;
            this.Deadline = task.Deadline;
            this.StartDate = task.StartDate;
            this.Category = task.Category;
            this.Created = task.Created;
            this.Changed = task.Changed;
            this.Effort = task.Effort;
            this.Progress = task.Progress;
            this.StatusNum = task.StatusNum;
        }

        /// <summary>
        /// Convert all properties of the class to string and concatinate them seperated by a comma
        /// </summary>
        /// <returns>Returns all properties seperated with a ", " as a string</returns>
        public override string ToString()
        {
            return Title?.ToString() + ", " +
                Description?.ToString() + ", " +
                Enum.GetName(typeof(Priority), PriorityNum) + ", " +
                Deadline.ToString() + ", " +
                StartDate.ToString() + ", " +
                Category?.ToString() + ", " +
                Created.ToString() + ", " +
                Changed.ToString() + ", " +
                Effort.ToString() + ", " +
                Progress.ToString() + ", " +
                Enum.GetName(typeof(Status), StatusNum);
        }
        #endregion
    }
}