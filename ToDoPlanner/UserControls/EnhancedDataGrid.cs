using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ToDoPlanner.UserControls
{
    class EnhancedDataGrid : DataGrid
    {
        /// <summary>
        /// Original Source from:
        /// https://bengribaudo.com/blog/2012/03/14/1942/saving-restoring-wpf-datagrid-columns-size-sorting-and-order
        /// 
        /// Added comments and Visibility tracking of each column.
        /// Some other small fixes and improvements.
        /// </summary>

        private bool inWidthChange = false;
        private bool updatingColumnInfo = false;

        public static readonly DependencyProperty ColumnInfoProperty = DependencyProperty.Register("ColumnInfo",
                typeof(ObservableCollection<ColumnInfo>), typeof(EnhancedDataGrid),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, ColumnInfoChangedCallback)
            );
        
        private static void ColumnInfoChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var grid = (EnhancedDataGrid)dependencyObject;
            if (!grid.updatingColumnInfo) { grid.ColumnInfoChanged(); }
        }

        /// <summary>
        /// Initialize the enhanced datagrid.
        /// 
        /// Setting up all events to detect changes in every column for width, sort order and visibility
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInitialized(EventArgs e)
        {
            
            EventHandler sortDirectionChangedHandler = (sender, x) => UpdateColumnInfo();
            EventHandler widthPropertyChangedHandler = (sender, x) => inWidthChange = true;
            EventHandler visiblityChangedHandler = (sender, x) => UpdateColumnInfo();
            var sortDirectionPropertyDescriptor = DependencyPropertyDescriptor.FromProperty(DataGridColumn.SortDirectionProperty, typeof(DataGridColumn));
            var widthPropertyDescriptor = DependencyPropertyDescriptor.FromProperty(DataGridColumn.WidthProperty, typeof(DataGridColumn));
            var visibilityPropertyDescriptor = DependencyPropertyDescriptor.FromProperty(DataGridColumn.VisibilityProperty, typeof(DataGridColumn));
            
            // Add events to each datagrid loaded, for detecting changes in width, sort order and visibility of each column
            Loaded += (sender, x) =>
            {
                foreach (var column in Columns)
                {
                    sortDirectionPropertyDescriptor.AddValueChanged(column, sortDirectionChangedHandler);
                    widthPropertyDescriptor.AddValueChanged(column, widthPropertyChangedHandler);
                    visibilityPropertyDescriptor.AddValueChanged(column, visiblityChangedHandler);
                    
                }
            };
            // Remove events when a datagrid is unloaded
            Unloaded += (sender, x) =>
            {
                foreach (var column in Columns)
                {
                    sortDirectionPropertyDescriptor.RemoveValueChanged(column, sortDirectionChangedHandler);
                    widthPropertyDescriptor.RemoveValueChanged(column, widthPropertyChangedHandler);
                    visibilityPropertyDescriptor.RemoveValueChanged(column, visiblityChangedHandler);
                }
            };
            base.OnInitialized(e);
        }

        // 
        public ObservableCollection<ColumnInfo> ColumnInfo
        {
            get { return (ObservableCollection<ColumnInfo>)GetValue(ColumnInfoProperty); }
            set { SetValue(ColumnInfoProperty, value); }
        }

        /// <summary>
        /// Update ColumnInfo whenever some settings are changed
        /// </summary>
        private void UpdateColumnInfo()
        {
            updatingColumnInfo = true;
            ColumnInfo = new ObservableCollection<ColumnInfo>(Columns.Select((x) => new ColumnInfo(x)));
            updatingColumnInfo = false;
        }

        /// <summary>
        /// Updates after the column order is changed
        /// </summary>
        /// <param name="e"></param>
        protected override void OnColumnReordered(DataGridColumnEventArgs e)
        {
            UpdateColumnInfo();
            base.OnColumnReordered(e);
        }

        /// <summary>
        /// Check on left mouse button reales, if any column width is changed and update if so
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreviewMouseLeftButtonUp(System.Windows.Input.MouseButtonEventArgs e)
        {
            if (inWidthChange)
            {
                inWidthChange = false;
                UpdateColumnInfo();
            }
            base.OnPreviewMouseLeftButtonUp(e);
        }

        /// <summary>
        /// Apply the changes from ColumnInfo to the grid
        /// </summary>
        private void ColumnInfoChanged()
        {
            Items.SortDescriptions.Clear();
            foreach (var column in ColumnInfo)
            {
                var realColumn = Columns.Where((x) => column.Header.Equals(x.Header)).FirstOrDefault();
                if (realColumn == null) { continue; }
                column.Apply(realColumn, Columns.Count, Items.SortDescriptions);
            }
        }
    }

    public struct ColumnInfo
    {
        public object Header;                       // Header, e.g. title string     
        public string PropertyPath;                 // Path to the source
        public Visibility Visibility;               // Visibility of the column
        public ListSortDirection? SortDirection;    // Sort direction (Accending, Decending)
        public int DisplayIndex;                    // The position of the column. 0 = left, 1  = 2nd, 2 = 3rd
        public double WidthValue;                   // Width of the column
        public DataGridLengthUnitType WidthType;    // Width type (Pixel, Star, Auto...)

        public ColumnInfo(DataGridColumn column)
        {
            Header = column.Header;
            //PropertyPath = ((Binding)((DataGridBoundColumn)column).Binding).Path.Path;
            PropertyPath = column.SortMemberPath;
            WidthValue = column.Width.DisplayValue;
            WidthType = column.Width.UnitType;
            Visibility = column.Visibility;
            SortDirection = column.SortDirection;
            DisplayIndex = column.DisplayIndex;
        }

        /// <summary>
        /// Apply settings to columns
        /// </summary>
        /// <param name="column"></param>The actual column to be modified
        /// <param name="gridColumnCount"></param>The position of the actual column
        /// <param name="sortDescriptions"></param>The sort setting (Accending, Decending)
        public void Apply(DataGridColumn column, int gridColumnCount, SortDescriptionCollection sortDescriptions)
        {
            column.Width = new DataGridLength(WidthValue, WidthType);
            column.SetCurrentValue(DataGridColumn.VisibilityProperty, Visibility);
            column.SortDirection = SortDirection;
            if (SortDirection != null)
            {
                sortDescriptions.Add(new SortDescription(PropertyPath, SortDirection.Value));
            }
            if (column.DisplayIndex != DisplayIndex)
            {
                var maxIndex = (gridColumnCount == 0) ? 0 : gridColumnCount - 1;
                column.DisplayIndex = (DisplayIndex <= maxIndex) ? DisplayIndex : maxIndex;
            }
        }
    }
}
