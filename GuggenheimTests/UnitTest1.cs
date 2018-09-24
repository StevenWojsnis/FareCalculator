using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using GuggenheimTest.Services;
using GuggenheimTest.DataModels;

namespace GuggenheimTests
{
    [TestClass]
    public class FareCalculatorServiceTests
    {
        [TestMethod]
        public void PeakSurchargeWeekdayPeakTimeTest()
        {
            //Arrange
            FareCalculatorService calculator = new FareCalculatorService();
            PrivateObject privateCalculator = new PrivateObject(calculator);

            // Is a Thursday
            var date = new DateTime(2018, 9, 20, 16, 0, 0);

            //Act
            var fare = privateCalculator.Invoke("PeakTimeSurcharge", date, date);
            //Assert
            Assert.AreEqual((decimal) 1.00, (decimal) fare);
        }

        [TestMethod]
        public void PeakSurchargeWeekdayOffPeakTimeTest()
        {
            //Arrange
            FareCalculatorService calculator = new FareCalculatorService();
            PrivateObject privateCalculator = new PrivateObject(calculator);

            // Is a Thursday
            var date = new DateTime(2018, 9, 20, 21, 0, 0);

            //Act
            var fare = privateCalculator.Invoke("PeakTimeSurcharge", date, date);
            //Assert
            Assert.AreEqual(0, (decimal) fare);
        }

        [TestMethod]
        public void PeakSurchargeWeekendPeakTimeTest()
        {
            //Arrange
            FareCalculatorService calculator = new FareCalculatorService();
            PrivateObject privateCalculator = new PrivateObject(calculator);

            // Is a Saturday
            var date = new DateTime(2018, 9, 22, 16, 0, 0);

            //Act
            var fare = privateCalculator.Invoke("PeakTimeSurcharge", date, date);
            //Assert
            Assert.AreEqual(0, (decimal) fare);
        }

        [TestMethod]
        public void PeakSurchargeWeekendOffPeakTimeTest()
        {
            //Arrange
            FareCalculatorService calculator = new FareCalculatorService();
            PrivateObject privateCalculator = new PrivateObject(calculator);

            // Is a Saturday
            var date = new DateTime(2018, 9, 22, 21, 0, 0);

            //Act
            var fare = privateCalculator.Invoke("PeakTimeSurcharge", date, date);
            //Assert
            Assert.AreEqual(0, (decimal) fare);
        }

        [TestMethod]
        public void NightSurchargeDuringNightTimeTest()
        {
            //Arrange
            FareCalculatorService calculator = new FareCalculatorService();
            PrivateObject privateCalculator = new PrivateObject(calculator);
            var date = new DateTime(2018, 9, 20, 20, 0, 0);

            //Act
            var fare = privateCalculator.Invoke("NightTimeSurcharge", date);
            //Assert
            Assert.AreEqual((decimal) fare, (decimal) 0.50);
        }

        [TestMethod]
        public void NightSurchargeDuringDayTimeTest()
        {
            //Arrange
            FareCalculatorService calculator = new FareCalculatorService();
            PrivateObject privateCalculator = new PrivateObject(calculator);
            var date = new DateTime(2018, 9, 20, 7, 0, 0);

            //Act
            var fare = privateCalculator.Invoke("NightTimeSurcharge", date);
            //Assert
            Assert.AreEqual((decimal) fare, 0);
        }

        [TestMethod]
        public void MinutesAboveSixMphCalculationTest()
        {
            //Arrange
            FareCalculatorService calculator = new FareCalculatorService();
            PrivateObject privateCalculator = new PrivateObject(calculator);
            var minutes = 10;

            //Act
            var fare = privateCalculator.Invoke("PriceFromMinutesAboveSixMph", minutes);
            //Assert
            Assert.AreEqual((decimal) fare, (decimal) 3.50);
        }

        [TestMethod]
        public void DistanceUnitsBelowSixMphCalculationTest()
        {
            //Arrange
            FareCalculatorService calculator = new FareCalculatorService();
            PrivateObject privateCalculator = new PrivateObject(calculator);
            var distanceUnits = 3;

            //Act
            var fare = privateCalculator.Invoke("PriceFromDistanceUnitsBelowSixMph", distanceUnits);
            //Assert
            Assert.AreEqual((decimal) fare, (decimal) 1.05);
        }

        [TestMethod]
        public void CalculateFareTest()
        {
            //Arrange
            FareCalculatorService calculator = new FareCalculatorService();
            FareCalculatorDataModel model = new FareCalculatorDataModel
            {
                Date = new DateTime(2018, 9, 20, 16, 0, 0), // Thursday
                Time = new DateTime(2018, 9, 20, 16, 0, 0), // Peak Time
                DistanceUnitsBelowSixMph = 10,
                MinutesAboveSixMph = 5,
            };

            //Act
            var fare = calculator.CalculateFare(model);
            //Assert
            Assert.AreEqual((decimal)fare, (decimal) 9.75);
        }
    }
}
