using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoPlanner.ViewModel;

namespace ToDoPlannerTest
{
    [TestClass]
    public class PercentageValueConverterTest
    {
        [TestMethod]
        public void Convert_ValidValue_PercentageSymbolAdded()
        {
            var converter = new PercentageValueConverter();
            int value = 10;
            object result;

            result = converter.Convert(value, null, null, null);
            Assert.AreEqual(result, value + "%");
        }

        [TestMethod]
        public void Convert_ValidParameter_ValueLimitedToLowerValue()
        {
            var converter = new PercentageValueConverter();
            int value = -100;
            int lowerLimit = -50;
            int higherLimit = 200;
            object result;
            string parameter = lowerLimit + "," + higherLimit;

            result = converter.Convert(value, null, parameter, null);
            Assert.AreEqual(result, lowerLimit + "%");
        }

        [TestMethod]
        public void Convert_ValidParameter_ValueLimitedToHigherValue()
        {
            var converter = new PercentageValueConverter();
            int value = 500;
            int lowerLimit = -50;
            int higherLimit = 200;
            object result;
            string parameter = lowerLimit + "," + higherLimit;

            result = converter.Convert(value, null, parameter, null);
            Assert.AreEqual(result, higherLimit + "%");
        }
    }
}
