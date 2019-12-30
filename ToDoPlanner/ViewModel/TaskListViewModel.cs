using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.IO;
using System.Xml.Serialization;
using ToDoPlanner.Annotations;
using ToDoPlanner.Command;
using ToDoPlanner.Model;
using ToDoPlanner.UserControls;
using System.Linq;

namespace ToDoPlanner.ViewModel
{
    public class TaskListViewModel : ViewModelBase
    {

        #region Properties

        /// <summary>
        /// The task list
        /// </summary>
        public ObservableCollection<ToDoTask> ToDoTasks { get; set; }

        /// <summary>
        /// The settings of a datagrid.
        /// Hold information about the width and visibility of the columns
        /// </summary>
        private ObservableCollection<ColumnInfo> columnInfos;
        public ObservableCollection<ColumnInfo> ColumnInfos { get; set; }

        public TaskViewModel TaskViewModelControl { get; set; }

        private ToDoTask selectedTask;


        public ToDoTask SelectedTask {
            get { return selectedTask; }
            set
            {
                TaskViewModelControl.Task = value;
                selectedTask = value;
            }
        }

        #endregion

        #region Constants

        private const string FolderPathData = "Data";
        private const string FileNameTasks = "Tasks.xml";

        private const string FolderPathSettings = "Settings";
        private const string FileNameDataGridView = "DataGridView.xml";

        #endregion

        #region Commands

        /// <summary>
        /// Command for adding a new task
        /// </summary>
        public RelayCommand AddNewTaskCoomand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Basic contsructor of the TaskListViewModel
        /// </summary>
        public TaskListViewModel()
        {
            TaskViewModelControl = new TaskViewModel(this);
            AddNewTaskCoomand = new RelayCommand(AddNewTask);
        }

        #endregion

        public void LoadTasks()
        {
            ObservableCollection<ToDoTask> toDoTasks = new ObservableCollection<ToDoTask>();
            ObservableCollection<ToDoTask> toDoTasks2 = new ObservableCollection<ToDoTask>();


            // Load DataGrid view settings from xml file
            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(ObservableCollection<ColumnInfo>));
                using (StreamReader rd = new StreamReader(FolderPathSettings + @"\" + FileNameDataGridView))
                {
                    columnInfos = xs.Deserialize(rd) as ObservableCollection<ColumnInfo>;
                    ColumnInfos = columnInfos;
                }
            }
            catch
            {
                System.Diagnostics.Trace.WriteLine("XML file Settings.xml not found");
            }


            // Load Tasks from xml file
            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(ObservableCollection<ToDoTask>));
                using (StreamReader rd = new StreamReader(FolderPathData + @"\" + FileNameTasks))
                {
                    toDoTasks = xs.Deserialize(rd) as ObservableCollection<ToDoTask>;
                    ToDoTasks = toDoTasks;
                }
            }
            catch
            {
                System.Diagnostics.Trace.WriteLine("XML file Tasks.xml not found");
            }
        }

        public void AddTask(ToDoTask task)
        {
            ToDoTasks.Add(task);
        }

        private void AddNewTask()
        {
            var toDoTask = new ToDoTask();
            ToDoTasks.Add(toDoTask);
            SelectedTask = toDoTask;
        }

        /// <summary>
        /// Save tasks to .xml file
        /// </summary>
        public void SaveTasks()
        {
            var serializer = new XmlSerializer(typeof(ObservableCollection<ToDoTask>));
            if (!Directory.Exists(FolderPathData))
            {
                try
                {
                    Directory.CreateDirectory(FolderPathData);
                }
                catch
                {
                    System.Diagnostics.Trace.WriteLine("Directory couldn't be created");
                }
            }

            try
            {
                FileStream fs = new FileStream(FolderPathData + @"\" + FileNameTasks, FileMode.Create);
                serializer.Serialize(fs, ToDoTasks);
                fs.Close();
            }
            catch
            {
                System.Diagnostics.Trace.WriteLine("File couldn't be created");
            }

        }

        /// <summary>
        /// Save DataGrid view settings to .xml file
        /// 
        /// Saves the actual order, width and visibility of the columns
        /// </summary>
        public void SaveSettings()
        {
            var serializer = new XmlSerializer(typeof(ObservableCollection<ColumnInfo>));
            if (!Directory.Exists(FolderPathSettings))
            {
                try
                {
                    Directory.CreateDirectory(FolderPathSettings);
                }
                catch
                {
                    System.Diagnostics.Trace.WriteLine("Directory couldn't be created");
                }
            }

            try
            {
                FileStream fs = new FileStream(FolderPathSettings + @"\" + FileNameDataGridView, FileMode.Create);
                serializer.Serialize(fs, ColumnInfos);
                fs.Close();
            }
            catch
            {
                System.Diagnostics.Trace.WriteLine("File couldn't be created");
            }

        }

        public void Close(object sender, CancelEventArgs e)
        {
            SaveTasks();
            SaveSettings();
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