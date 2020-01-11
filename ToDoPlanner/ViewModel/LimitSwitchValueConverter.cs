using System;
using System.Globalization;
using System.Windows.Data;

namespace ToDoPlanner.ViewModel
{
    class LimitSwitchValueConverter : IValueConverter
    {

        /// <summary>
        /// Add a percentage symbol to the end of an value.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">A converted value. If the method returns null, the valid null value is used.</param>
        /// <returns>A string with a percentage symbol in the end.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string para = parameter as string;

            if(para != null)
            {
                string[] parameters = para.Split(',');
                if(parameters.Length == 3)
                {
                    int limit = System.Convert.ToInt32(parameters[0]);
                    int lowValue = System.Convert.ToInt32(parameters[1]);
                    int highValue = System.Convert.ToInt32(parameters[2]);

                    return ((limit < System.Convert.ToInt32(value)) ? highValue : lowValue);
                }
            }
            return 1;
        }

        /// <summary>
        /// Remove percentage symbols from a the value string and convert to an int
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>An interger without percentage symbol</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
