///------------------------------------------------------------------------
/// Namespace:    ToDoPlanner.ViewModel
/// Class:        PercentageValueConverter
/// Author:       Kevin Kessler & Regula Engelhardt
/// Copyright:    (c) Kevin Kessler & Regula Engelhardt
///------------------------------------------------------------------------

using System;
using System.Globalization;
using System.Windows.Data;

namespace ToDoPlanner.ViewModel
{
    /// <summary>
    /// A Converter to add a percentage symbol to and int or removing it from a string and convert it to an int.
    /// Also limiting the value from 0 to 100, or alternative the values can be changed with the us of the parameter.
    /// </summary>
    public class PercentageValueConverter : IValueConverter

    {
        #region Default values

        const long defaultLowerLimit = 0;
        const long defaultUpperLimit = 100;

        #endregion

        #region IValueConverter methods

        /// <summary>
        /// Add a percentage symbol to the end of a value.
        /// </summary>
        /// <param name="value">The value produced by the binding source. Limit from 0 to 100 if no paramters are used.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use. Possible Paramter "lowerLimit,upperLimit", e.g. "-50,250" = limit the value from -50 to 200.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A string with a percentage symbol in the end. If conversation fails return "0%"</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            long lowerLimit = defaultLowerLimit;
            long upperLimit = defaultUpperLimit;

            try
            {
                string para = parameter as string;

                if (para != null)
                {
                    string[] parameters = para.Split(',');
                    if (parameters.Length == 2)
                    {
                        lowerLimit = System.Convert.ToInt64(parameters[0]);
                        upperLimit = System.Convert.ToInt64(parameters[1]);
                    }
                }

                long tempVal = System.Convert.ToInt64(value);
                tempVal = (tempVal > upperLimit) ? upperLimit : tempVal;
                tempVal = (tempVal < lowerLimit) ? lowerLimit : tempVal;

                return System.Convert.ToString(tempVal) + '%';
            }
            catch
            {
                return "0%";
            }
        }

        /// <summary>
        /// Remove percentage symbols from the value string and convert to an int. Limit from 0 to 100 if no paramters are used.
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use. Possible Paramter "lowerLimit,upperLimit", e.g. "-50,250" = limit the value from -50 to 200.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>An interger without percentage symbol. If conversation fails return 0</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            long lowerLimit = defaultLowerLimit;
            long upperLimit = defaultUpperLimit;

            try
            {
                string para = parameter as string;

                if (para != null)
                {
                    string[] parameters = para.Split(',');
                    if (parameters.Length == 2)
                    {
                        lowerLimit = System.Convert.ToInt64(parameters[0]);
                        upperLimit = System.Convert.ToInt64(parameters[1]);
                    }
                }

                long tempVal = System.Convert.ToInt64(((string)value).Trim('%'));
                tempVal = (tempVal > upperLimit) ? upperLimit : tempVal;
                tempVal = (tempVal < lowerLimit) ? lowerLimit : tempVal;

                return tempVal;
            }
            catch
            {
                return 0;
            }
        }
        #endregion
    }
}
