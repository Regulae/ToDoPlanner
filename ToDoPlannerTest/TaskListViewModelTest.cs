using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoPlanner.ViewModel;
using ToDoPlanner.Model;
using ToDoPlanner.UserControls;
using System.IO;

namespace ToDoPlannerTest
{
    [TestClass]
    public class TaskListViewModelTest
    {
        /// <summary>
        /// Test if a task is added with the AddTask method
        /// </summary>
        [TestMethod]
        public void AddTask_TaskIsAdded_ReturnsTrue()
        {
            // Creat a new TaskListView and add an empty list
            var taskList = new TaskListViewModel();
            taskList.ToDoTasks = new System.Collections.ObjectModel.ObservableCollection<ToDoTask>();
            
            // Creat a task
            var task = new ToDoTask();

            // Make sure the task is not in the list and the Contains method work ass intended
            if (taskList.ToDoTasks.Contains(task))
            {
                Assert.Fail("Task already included before added");
                return;
            }

            // Add the task
            taskList.AddTask(task);

            // Check if the task is in the list
            Assert.IsTrue(taskList.ToDoTasks.Contains(task));

        }

        /// <summary>
        /// Test if settings can be saved and loaded
        /// </summary>
        [TestMethod]
        public void SaveLoadSettings_SaveAndLoadingSettings_ReturnsTrue()
        {
            // Creat a new TaskListView and add an empty list
            var taskList = new TaskListViewModel();
            taskList.ColumnInfos = new System.Collections.ObjectModel.ObservableCollection<ColumnInfo>();

            // Creat example settings
            var settings = new ColumnInfo()
            {
                WidthType = System.Windows.Controls.DataGridLengthUnitType.Pixel,
                WidthValue = 100.0,
                DisplayIndex = 0,
                Header = "TestTitle",
                PropertyPath = "TestPath",
                SortDirection = System.ComponentModel.ListSortDirection.Ascending,
                Visibility = System.Windows.Visibility.Collapsed

            };

            // Add the settings
            taskList.ColumnInfos.Add(settings);

            // Try to save the settings
            try
            {
                taskList.SaveSettings();
            }
            catch
            {
                Assert.Fail("Couldn't save the settings");
                return;
            }

            // Delete the current settings
            taskList.ColumnInfos = null;

            // Load the settings
            try
            {
                taskList.LoadSettings();
            }
            catch
            {
                Assert.Fail("Couldn't load the settings");
                return;
            }

            // Check if the settings loaded are equal to the settings saved.
            Assert.IsTrue(taskList.ColumnInfos[0].Equals(settings), "The loaded settings are not equal");

        }

    }
}
