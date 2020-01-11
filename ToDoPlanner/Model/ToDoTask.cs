using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;
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
        private int? _id;
        
        [JsonProperty("id")]
        public int? Id
        {
            get => _id ?? null;
            set => SetProperty(ref _id, value);
        }
        
        private string _title;
        [JsonProperty("title")]
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private string _description;
        
        [JsonProperty("description")]
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        private int _priorityNum;
        
        [JsonProperty("priority")]
        public int PriorityNum
        {
            get => _priorityNum;
            set => SetProperty(ref _priorityNum, value);
        }

        //public int status = 1;
        private int _statusNum;

        [JsonProperty("status")]
        public int StatusNum
        {
            get => _statusNum;
            set => SetProperty(ref _statusNum, value);
        }

        private DateTime _deadline = DateTime.Today;
        [JsonProperty("deadline")]
        public DateTime Deadline
        {
            get => _deadline;
            set => SetProperty(ref _deadline, value);
        }

        private DateTime _startDate = DateTime.Today;
        [JsonProperty("start_date")]
        public DateTime StartDate
        {
            get => _startDate;
            set => SetProperty(ref _startDate, value);
        }

        private string _category;

        [JsonProperty("category")]
        public string Category
        {
            get => _category;
            set => SetProperty(ref _category, value);
        }

        private DateTime _created = DateTime.Today;
        
        [JsonProperty("created")]
        public DateTime Created
        {
            get => _created;
            set => SetProperty(ref _created, value);
        }

        private DateTime _changed = DateTime.Today;
        
        [JsonProperty("changed")]
        public DateTime Changed
        {
            get => _changed;
            set => SetProperty(ref _changed, value);
        }

        private int _effort;
        [JsonProperty("effort")]
        public int Effort
        {
            get => _effort;
            set => SetProperty(ref _effort, value);
        }

        private int _progress;
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
        /// <returns></returns>
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
                Progress = this.Progress
            };
        }

        /// <summary>
        /// Copy all values and references
        /// </summary>
        /// <param name="task"></param>
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
        }

        #endregion
    }
}