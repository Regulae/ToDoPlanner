using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ToDoPlanner.Model
{
    /// <summary>
    /// Class for binding between diffrent data context
    /// </summary>
    public class BindingProxy : Freezable
    {
        public static readonly DependencyProperty DataProperty =
           DependencyProperty.Register("Data", typeof(object),
              typeof(BindingProxy), new UIPropertyMetadata(null));

        /// <summary>
        /// Container for an object which can be bound
        /// </summary>
        public object Data
        {
            get { Console.WriteLine("Proxy Used"); return GetValue(DataProperty); }
            set { SetValue(DataProperty, value); Console.WriteLine("Proxy Set"); }
        }

        #region Overrides of Freezable

        protected override Freezable CreateInstanceCore()
        {
            return new BindingProxy();
        }

        #endregion
    }
}
