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
    public class TaskModel : ViewModelBase
    {

        public ObservableCollection<ToDoTask> ToDoTasks { get; set; }

        private ObservableCollection<ColumnInfo> columnInfos;
        public ObservableCollection<ColumnInfo> ColumnInfos { get; set; }

        private const string FolderPathData = "Data";
        private const string FileNameTasks = "Tasks.xml";

        private const string FolderPathSettings = "Settings";
        private const string FileNameDataGridView = "DataGridView.xml";

        private bool visibilityCreated = true;
        public bool VisibilityCreated { get { Console.WriteLine("Visi Get"); return visibilityCreated; } set { value = visibilityCreated; Console.WriteLine("Visi Changed"); } }

        private bool visibilityDeadline = true;
        public bool VisibilityDeadline { get { return visibilityDeadline; } set { value = visibilityDeadline; Console.WriteLine("Visi visibilityDeadline Changed"); } }

        private bool visibilityTitle = false;
        public bool VisibilityTitle { get { return visibilityTitle; } set { value = visibilityTitle; visibilityChanged(); Console.WriteLine("Visi visibilityTitle Changed"); } }


        private void visibilityChanged()
        {
            var found = ColumnInfos.FirstOrDefault(x => string.Equals(x.Header, "Title"));
            found.Visibility = System.Windows.Visibility.Collapsed;
            int i = 10;
        }

        public void LoadTasks()
        {
            ObservableCollection<ToDoTask> toDoTasks = new ObservableCollection<ToDoTask>();
            //ObservableCollection<ColumnInfo> columnInfos = new ObservableCollection<ColumnInfo>();


            // Load DataGrid view settings from xml file
            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(ObservableCollection<ColumnInfo>));
                using (StreamReader rd = new StreamReader(FolderPathSettings + @"\" + FileNameDataGridView))
                {
                    //columnInfos = ColumnInfos;
                    columnInfos = xs.Deserialize(rd) as ObservableCollection<ColumnInfo>;
                    ColumnInfos = columnInfos;
                    //ColumnInfos.Clear();

                    //foreach (ColumnInfo ci in columnInfos)
                    //{
                    //    ColumnInfos.Add(ci);
                    //}

                    Console.WriteLine("This is the Collection");
                    Console.WriteLine(ColumnInfos);

                    
                }
            }
            catch
            {
                System.Diagnostics.Trace.WriteLine("XML file Tasks.xml not found");
            }


            // Load Tasks from xml file
            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(ObservableCollection<ToDoTask>));
                using (StreamReader rd = new StreamReader(FolderPathData + @"\" + FileNameTasks))
                {
                    toDoTasks = xs.Deserialize(rd) as ObservableCollection<ToDoTask>;
                }
            }
            catch
            {
                System.Diagnostics.Trace.WriteLine("XML file Tasks.xml not found");
            }

            //toDoTasks.Add(new ToDoTask()
            //{
            //    // Input from User
            //    Title = "Task Example",
            //    Description = "An exemplary task to get the idea.",
            //    PriorityNum = (int)Priority.High,
            //    Deadline = DateTime.Parse("09.12.2019"),
            //    // DeadlineString = Deadline.ToString("dd.MM.yyyy"),
            //    StartDate = DateTime.Parse("23.01.2020"),
            //    Category = "C# Project",
            //    Effort = 50,
            //    Progress = 10,

            //    // Generated Input from System
            //    Created = DateTime.Today,
            //    Changed = DateTime.Today
            //});

            //toDoTasks.Add(new ToDoTask()
            //{
            //    // Input from User
            //    Title = "ComboBox Priority",
            //    Description = "The ComboBox for the priority don't change the value in the list or will not be updated",
            //    PriorityNum = (int)Priority.Medium,
            //    Deadline = DateTime.Parse("12.12.2019"),
            //    // DeadlineString = Deadline.ToString("dd.MM.yyyy"),
            //    StartDate = DateTime.Parse("05.12.2019"),
            //    Category = "C# Project",
            //    Effort = 50,
            //    Progress = 10,

            //    // Generated Input from System
            //    Created = DateTime.Today,
            //    Changed = DateTime.Today
            //});

            //toDoTasks.Add(new ToDoTask()
            //{
            //    // Input from User
            //    Title = "Another Example for testing how long the text has to be to be shown on the datagrid",
            //    Description = "What else could we do?",
            //    PriorityNum = (int)Priority.Low,
            //    Deadline = DateTime.Parse("05.03.2020"),
            //    // DeadlineString = Deadline.ToString("dd.MM.yyyy"),
            //    StartDate = DateTime.Parse("23.01.2020"),
            //    Category = "C# Project",
            //    Effort = 50,
            //    Progress = 10,

            //    // Generated Input from System
            //    Created = DateTime.Today,
            //    Changed = DateTime.Today
            //});

            ToDoTasks = toDoTasks;

        }

        public void AddTask(ToDoTask task)
        {
            ToDoTasks.Add(task);
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