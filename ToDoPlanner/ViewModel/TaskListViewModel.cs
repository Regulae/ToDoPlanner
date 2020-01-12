using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;
using ToDoPlanner.Command;
using ToDoPlanner.Model;
using ToDoPlanner.UserControls;
using ToDoPlanner.Operations;
using System.Windows.Data;

namespace ToDoPlanner.ViewModel
{
    /// <summary>
    /// This view model class is about the task list.
    /// It loads/saves the tasks and the data grid view settings from/to .xml file
    /// The view of a single task is also instantiated in this class
    /// </summary>
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

        /// <summary>
        /// Task view to display details of a task
        /// </summary>
        public TaskViewModel TaskViewModelControl { get; set; }

        /// <summary>
        /// The actual selected task in the data grid
        /// </summary>
        private ToDoTask selectedTask;
        public ToDoTask SelectedTask {
            get { return selectedTask; }
            set
            {
                if (value != selectedTask)
                {
                    // If a new task has been selected, not only change the actual selected task
                    // also refresh the task of the task view
                    TaskViewModelControl.Task = value;
                    selectedTask = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// DataGrid View for filtered values
        /// </summary>
        private ICollectionView filteredView;

        /// <summary>
        /// Filter, to search in the task list, which task contains this filter string
        /// </summary>
        private string filter;
        public string Filter
        {
            get
            {
                return filter;
            }
            set
            {
                if (SetProperty(ref filter, value))
                    filteredView.Refresh();
            }
        }


        private ToDoTaskVisibility visibilityTaskList;
        public ToDoTaskVisibility VisibilityTaskList
        {
            get => visibilityTaskList;
            set => SetProperty(ref visibilityTaskList, value);
        }

        #endregion

        #region Constants

        /// <summary>
        /// Constant strings for saving/loading the tasks and the datagrid settings
        /// </summary>
        private const string FolderPathData = "Data";
        private const string FileNameTasks = "Tasks.xml";
        private const string FolderPathSettings = "Settings";
        private const string FileNameDataGridView = "DataGridView.xml";

        #endregion

        #region Commands

        /// <summary>
        /// Command for adding a new task
        /// </summary>
        public RelayCommand AddNewTaskCommand { get; set; }

        /// <summary>
        /// Command for deleting the selected task
        /// </summary>
        public RelayCommand DeleteTaskCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Basic contsructor of the TaskListViewModel
        /// </summary>
        public TaskListViewModel()
        {
            TaskViewModelControl = new TaskViewModel(this);
            AddNewTaskCommand = new RelayCommand(AddNewTask);
            DeleteTaskCommand = new RelayCommand(DeleteTask);
        }

        #endregion

        #region Helper methods

        /// <summary>
        /// Add a task to the list, if it's not already inside
        /// </summary>
        /// <param name="task"></param>
        public void AddTask(ToDoTask task)
        {
            if (!ToDoTasks.Contains(task))
            {
                ToDoTasks.Add(task);
                //Get token for authentication with api
                TokenResponse token = GetToken();
                ApiOperations ops = new ApiOperations();
                ops.PostTask(task, token);
            }

        }

        /// <summary>
        /// Delete the selected task in the datagrid
        /// </summary>
        private void DeleteTask()
        {
            ToDoTasks.Remove(selectedTask);
        }

        /// <summary>
        /// Add a new empty task to the list
        /// </summary>
        private void AddNewTask()
        {
            TaskViewModelControl.Task = new ToDoTask();
            TaskViewModelControl.HasChanged = true;
        }

        #endregion

        #region Loading / Initialize

        /// <summary>
        /// Load all settings and tasks from files
        /// </summary>
        public void Initialize()
        {
            LoadSettings();
            LoadTasks();
        }

        /// <summary>
        /// Load settings from .xml file for the datagrid column
        /// </summary>
        public void LoadSettings()
        {
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
        }

        /// <summary>
        /// Load the saved tasks from .xml file
        /// </summary>
        public void LoadTasks()
        {
            // Get token for authentication with api
            TokenResponse token = GetToken();
            ApiOperations ops = new ApiOperations();
            ToDoTasks = ops.GetTasks(token);
            filteredView = CollectionViewSource.GetDefaultView(ToDoTasks);
            filteredView.Filter = o => string.IsNullOrEmpty(Filter) ? true : (o.ToString()).Contains(Filter);
        }

        #endregion

        #region Saving / Closing
        
        /// <summary>
        /// Save the settings and tasks
        /// </summary>
        /// <param name="sender"></param> The event sende e.g. application or windows
        /// <param name="e"></param> The Arguments
        public void Close(object sender, CancelEventArgs e)
        {
            //SaveTasks();
            SaveSettings();
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

        #endregion
    }
}