using System;
using System.Collections.Generic;
using System.Linq;
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

            [TestMethod]
            public void MutatingStateFromConcurrentProcesses_UnpredictableResults()
            {
                //Arrange
                var nums = Enumerable.Range(-10000, 20001).ToList();

                int t1Sum = 89;
                int t2Sum  = 89;


                Action t1 = () =>  Console.WriteLine(nums.Sum());

                Action t2 = () => { nums.Sort(); Console.WriteLine(nums.Sum()); };

                //Act
                Parallel.Invoke(t1, t2);

                //Assert

            }
           
        }
    }
}
