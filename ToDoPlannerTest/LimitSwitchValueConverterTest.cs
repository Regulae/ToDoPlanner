using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoPlanner.ViewModel;
using System.Windows.Data;

namespace ToDoPlannerTest
{
    [TestClass]
    public class LimitSwitchValueConverterTest
    {
        const int defaultValue = 1;
        [TestMethod]
        public void Convert_PassEmptyParameter_DefaultValue()
        {
            var converter = new LimitSwitchValueConverter();
            int value = 10;
            object result;
            string parameter = "";

            result = converter.Convert(value, null, parameter, null);
            Assert.AreEqual(defaultValue, result);
        }


        [TestMethod]
        public void Convert_InvalidParameter_DefaultValue()
        {
            var converter = new LimitSwitchValueConverter();
            int value = 10;
            object result;
            string parameter = "foo";

            result = converter.Convert(value, null, parameter, null);
            Assert.AreEqual(defaultValue, result);
        }

        [TestMethod]
        public void Convert_ValueSamllerThanLimit_FirstValue()
        {
            var converter = new LimitSwitchValueConverter();
            int value = 10;
            int limit = 100;
            int firstValue = 2;
            int secondValue = 4;
            object result;
            string parameter = limit + "," + firstValue + "," + secondValue;

            result = converter.Convert(value, null, parameter, null);
            Assert.AreEqual(firstValue, result);
        }

        [TestMethod]
        public void Convert_ValueBiggerThanLimit_SecondValue()
        {
            var converter = new LimitSwitchValueConverter();
            float value = 1000;
            int limit = 100;
            int firstValue = 2;
            int secondValue = 4;
            object result;
            string parameter = limit + "," + firstValue + "," + secondValue;

            result = converter.Convert(value, null, parameter, null);
            Assert.AreEqual(secondValue, result);
        }

        [TestMethod]
        public void Convert_ValueEqualThanLimit_FirstValue()
        {
            var converter = new LimitSwitchValueConverter();
            float value = 100;
            int limit = 100;
            int firstValue = 2;
            int secondValue = 4;
            object result;
            string parameter = limit + "," + firstValue + "," + secondValue;

            result = converter.Convert(value, null, parameter, null);
            Assert.AreEqual(firstValue, result);
        }
    }
}
