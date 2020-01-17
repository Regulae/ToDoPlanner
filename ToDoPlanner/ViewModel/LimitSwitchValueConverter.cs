using System;
using System.Globalization;
using System.Windows.Data;

namespace ToDoPlanner.ViewModel
{
    public class LimitSwitchValueConverter : IValueConverter
    {

        /// <summary>
        /// Convert a passed value to either a defined first value or second value depending on the limit.
        /// Without parameter or invalid parameter returns 1.
        /// Parameter has to be passed like "limit,firstValue,secondValue" e.g. "500,2,4"
        /// Example: If the value passed is bigger than the limit(500) return first value(2), else return second value(4).
        /// </summary>
        /// <param name="value">The value as a number</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">Parameter has to be a string with 3 integer values, seperated by a ",", e.g. "500,2,4".
        /// First int is the limit
        /// Second int is the return value if the value passed is smaller or equal to the limit
        /// Third int is the return value if the passed value is bigger than the limit</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>Returns an int, either the high value, the low value or if the parameter are invalid returns 1</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string para = parameter as string;

            if(para != null)
            {
                // remove empty spaces
                para.Trim(' ');
                // seperate numbers
                string[] parameters = para.Split(',');

                // three numbers has to be passed, that the parameter are valid
                if(parameters.Length == 3)
                {
                    try
                    {
                        int limit = System.Convert.ToInt32(parameters[0]);
                        int firstValue = System.Convert.ToInt32(parameters[1]);
                        int secondValue = System.Convert.ToInt32(parameters[2]);

                        return ((limit < System.Convert.ToInt32(value)) ? secondValue : firstValue);
                    }
                    catch
                    {
                        System.Diagnostics.Trace.WriteLine("LimitSwitchValueConverter: Convert to int failed with parameter: " + parameter);
                    }

                }
                else
                {
                    System.Diagnostics.Trace.WriteLine("LimitSwitchValueConverter: Invalid parameter format passed with parameter:" + parameter);
                }
            }
            return 1;
        }

        /// <summary>
        /// Not able to convert back
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
