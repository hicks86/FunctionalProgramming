using System;

namespace BmiCalculator_Chapter2
{
    public class UserInterface
    {
        private readonly Action<string> _write;
        private readonly Func<string> _read;

        public UserInterface()
            : this(Console.WriteLine, Console.ReadLine)
        {

        }

        public UserInterface(Action<string> write, Func<string> read)
        {
            _write = write;
            _read = read;
        }
        public void PrintErrorMessage()
        {
            _write("Incorrect values");
        }

        public void WaitForAnyKeyPressed()
        {
            _write("Press ANY key to continue");

            _read();
        }

                
        public double ReadInput()
        {
            var value = _read;
            return Convert.ToDouble(value);
        }

        public void Print(string prompt)
        {
            _write(prompt);
        }
    }
}
