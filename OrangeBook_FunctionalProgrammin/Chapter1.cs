using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace OrangeBook_FunctionalProgrammin
{
    [TestClass]
    public class Chapter1
    {
        [TestClass]
        public class FunctionsAsFirstClassValues
        {
            [TestMethod]
            public void Example1()
            {
                //Arrange
                Func<int, int> triple = x => x * 3;

                var range = Enumerable.Range(1, 4);

                //Act
                var triples = range.Select(triple);

                //Assert
                triples.Should().BeEquivalentTo( 3, 6, 9, 12 );

            }
        }

        [TestClass]
        public class AvoidingStateMutation
        {
            public TestContext TestContext { get; set; }

            [TestMethod]
            public void FunctionalExample()
            {
                //Arrange
                Func<int, bool> isOdd = x => x % 2 == 1;
                int[] original = { 7, 6, 1 };

                //Act - Where and OrderBy do not affect the original list
                var sorted = original.OrderBy(x => x);
                var filtered = original.Where(isOdd);

                //Assert
                original.Should().BeEquivalentTo(7, 6, 1);
                sorted.Should().BeEquivalentTo(1, 6, 7);
                filtered.Should().BeEquivalentTo(7, 1);

            }

            [TestMethod]
            public void NonFunctionalExample()
            {
                //Arrange
                int[] original = { 7, 6, 1 };

                //Act - Sort affects the original list
                original.ToList().Sort();

                //Assert
                original.Should().BeEquivalentTo(1, 6, 7);
                
            }

            [Ignore]
            [TestMethod]
            public void MutatingStateFromConcurrentProcesses_UnpredictableResults()
            {
                //Arrange
                var writeLineStringBuilder = new StringBuilder();

                using (TextWriter writer = new StringWriter(writeLineStringBuilder))
                {
                    var nums = Enumerable.Range(-10000, 20001).ToList();

                    //Console.SetOut(writer);

                    Action t1 = () => Console.WriteLine(nums.Sum());

                    Action t2 = () => { nums.Sort(); Console.WriteLine(nums.Sum()); };

                    //Act
                    Parallel.Invoke(t2, t1);
                    
                    //Assert
                    string[] newline = new string[] { Environment.NewLine };
                    var results = writeLineStringBuilder.ToString().Split(newline, StringSplitOptions.RemoveEmptyEntries);

                    Convert.ToInt32(results[0]).Should().NotBe(0); //should be inconsistent
                    Convert.ToInt32(results[1]).Should().Be(0);
                }
            }

            [TestMethod]
            public void MutatingStateFromConcurrentProcesses_PredictableResults()
            {
                //Arrange
                var writeLineStringBuilder = new StringBuilder();

                using (TextWriter writer = new StringWriter(writeLineStringBuilder))
                {
                    var nums = Enumerable.Range(-10000, 20001).ToList();

                    Console.SetOut(writer);

                    Action t1 = () => Console.WriteLine(nums.Sum());

                    Action t3 = () => { Console.WriteLine(nums.OrderBy(x => x).Sum()); };

                    //Act
                    Parallel.Invoke(t1, t3);


                    //Assert
                    string[] newline = new string[] { Environment.NewLine };
                    var results = writeLineStringBuilder.ToString().Split(newline, StringSplitOptions.RemoveEmptyEntries);

                    Convert.ToInt32(results[0]).Should().Be(0);
                    Convert.ToInt32(results[1]).Should().Be(0);
                }



            }

        }

        [TestClass]
        public class HigherOrderFunctions
        {
            [TestMethod]
            public void AdapterFunctions()
            {
                //Arrange
                Func<double, double, double> divide = (x, y) => x / y;


                //Act
                var beforeSwap = divide(10, 2);

                var afterSwapDivideBy = divide.SwapArgs();
                var afterSwap = afterSwapDivideBy(10, 2);


                //Assert
                beforeSwap.Should().Be(5);
                afterSwap.Should().NotBe(5);
                afterSwap.Should().Be(0.2);


            }

            [TestMethod]
            public void FunctionsThatCreateFunctions()
            {
                //Arrange
                Func<int, bool> isModulus(int n) => i => (i % n == 0);

                //Act
                var range1 = Enumerable.Range(1, 20).Where(isModulus(2));
                var range2 = Enumerable.Range(1, 20).Where(isModulus(3));

                //Assert
                range1.Should().BeEquivalentTo(2, 4, 6, 8, 10, 12, 14, 16, 18, 20 );
                range2.Should().BeEquivalentTo(3,6,9,12,15,18);
            }


        }


    }

    public static class ExtensionsForTests
    {
        public static Func<T2, T1, R> SwapArgs<T2, T1, R>(this Func<T1, T2, R> f)
            => (t2, t1) => f(t1, t2);
    }

}
