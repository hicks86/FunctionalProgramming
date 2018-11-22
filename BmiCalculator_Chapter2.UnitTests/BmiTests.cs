using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BmiCalculator_Chapter2.UnitTests
{
    [TestClass]
    public class BmiTests
    {
        [DataTestMethod]
        [DataRow(1.80, 77, 23.77)]
        [DataRow(1.60, 77, 30.08)]
        [DataRow(1.77, 88.9, 28.38)]
        [DataRow(1.87, 60, 17.16)]
        public void CalculateBmi(double height, double weight, double expected)
        {
            //Arrange
            //Act
            var actual = BmiCalculator.Calculate(height, weight);

            //Assert
            actual.Should().Be(expected);
        }

        [DataTestMethod]
        [DataRow(23.77, BmiCategory.Healthy)]
        [DataRow(30.08, BmiCategory.Overweight)]
        [DataRow(28.38, BmiCategory.Overweight)]
        [DataRow(18.40, BmiCategory.Underweight)]
        public void CategorizeBmi(double bmi, BmiCategory expected)
        {
            //Arrange
            //Act
            var actual = bmi.Categorize();

            //Assert
            actual.Should().Be(expected);
        }

        [DataTestMethod]
        [DataRow(1.80, 77, BmiCategory.Healthy)]
        [DataRow(1.60, 77, BmiCategory.Overweight)]
        [DataRow(1.60, 77, BmiCategory.Overweight)]
        [DataRow(1.87, 60, BmiCategory.Underweight)]
        public void ReadBmi(double height, double weight, BmiCategory expected)
        {
            //Arrange
            var actual = default(BmiCategory);
            Func<string, double> read = s => s == "height" ? height : weight;
            Action<BmiCategory> write = r => actual = r;

            //Act
            BmiCalculator.Run(read, write);

            //Assert
            actual.Should().Be(expected);
        }
    }
}
