using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ToDoPlanner.Annotations;

namespace ToDoPlanner.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Title { get; set; }
        public string Description { get; set; }
        public enum Priority
        {
            Low, Medium, High
        }
        public int PriorityNum { get; set; }
        public DateTime Deadline  { get; set; }
        public DateTime StartDate  { get; set; }
        public string Category { get; set; }
        public enum Status
        {
            None, Open, Planned, Ongoing, Done
        }
        public DateTime Created { get; set; }
        public DateTime Changed { get; set; }
        public int Effort { get; set; }
        public int Progress { get; set; } // Implement as progressbar in the MainWindow.xaml

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
