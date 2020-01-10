using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Windows;

namespace ToDoPlanner.Model
{
    public class ToDoTaskVisibility : ModelBase
    {
        private bool title;

        public bool Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        private Visibility description;

        public Visibility Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        private Visibility priorityNum;
        public Visibility PriorityNum
        {
            get => priorityNum;
            set => SetProperty(ref priorityNum, value);
        }

        private Visibility deadline;
        public Visibility Deadline
        {
            get => deadline;
            set => SetProperty(ref deadline, value);
        }

        private Visibility startDate;
        public Visibility StartDate
        {
            get => startDate;
            set => SetProperty(ref startDate, value);
        }

        private Visibility category;
        public Visibility Category
        {
            get => category;
            set => SetProperty(ref category, value);
        }

        private Visibility created;
        public Visibility Created
        {
            get => created;
            set => SetProperty(ref created, value);
        }

        public Visibility Changed
        {
            get => category;
            set => SetProperty(ref category, value);
        }

        private Visibility effort;
        public Visibility Effort
        {
            get => effort;
            set => SetProperty(ref effort, value);
        }

        private Visibility progress;
        public Visibility Progress
        {
            get => progress;
            set => SetProperty(ref progress, value);
        }
    }
}
