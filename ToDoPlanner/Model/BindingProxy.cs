using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ToDoPlanner.Model
{
    public class BindingProxy : Freezable
    {
        public static readonly DependencyProperty DataProperty =
           DependencyProperty.Register("Data", typeof(object),
              typeof(BindingProxy), new UIPropertyMetadata(null));

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
