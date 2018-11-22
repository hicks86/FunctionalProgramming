using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BmiCalculator_Chapter2
{
    class Program
    {
        static void Main(string[] args)
        {
            var ui = new UserInterface();

            ui.Print("Welcome to the BMI Calculator" + Environment.NewLine);

            try
            {
                BmiCalculator.Run();

                ui.WaitForAnyKeyPressed();

            }
            catch (Exception)
            {
                ui.PrintErrorMessage();
            }
        }

    }
}
