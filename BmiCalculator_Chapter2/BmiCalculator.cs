using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BmiCalculator_Chapter2
{
    using static Math;
    using static System.Console;

    public enum BmiCategory { Underweight, Healthy, Overweight }


    public static class BmiCalculator
    {
        public static void Run()
        {
            Run(Read, Write);
        }

        public static void Run(Func<string, double> read, Action<BmiCategory> write)
        {
            // input
            double weight = read("weight")
                 , height = read("height");

            // computation
            var category = Categorize(Calculate(height, weight));

            // output
            write(category);
        }

        public static double Calculate(double height, double weight)
            => Round(weight / Pow(height,2), 2);

        public static BmiCategory Categorize(this double bmi)
        {
            return IsUnderweight(bmi) ? BmiCategory.Underweight
                   : IsOverweight(bmi) ? BmiCategory.Overweight
                   : BmiCategory.Healthy;
        }

        private static double Read(string field)
        {
            WriteLine($"Please enter your {field}");
            return double.Parse(ReadLine());
        }

        private static void Write(BmiCategory bmiRange)
           => WriteLine($"Based on your BMI, you are {bmiRange}");

        private static bool IsOverweight(double bmi)
            => bmi >= 25;

        private static bool IsUnderweight(double bmi)
            => bmi < 18.5;



    }
}
