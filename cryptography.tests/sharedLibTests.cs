using NUnit.Framework;
using System;

namespace cryptography.Tests
{
    [TestFixture]
    public class sharedLibTests
    {
        private SharedLib _sharedLib;

        [SetUp]
        public void Setup()
        {
            _sharedLib = new SharedLib();
        }

        [Test]
        [TestCase("hi",new[] {1, 2})]
        [TestCase("ten",new[] {1, 2})]
        [TestCase("5",new[] {1, 2})]
        [TestCase("2",new[] {1, 3, 4, 5})]
        public void validatorTestExpectedFailures(String option, int[] acceptedValues)
        {
            var result = SharedLib.validateOption(option, acceptedValues);
            Assert.IsFalse(result);
        }

        [Test]
        [TestCase("2",new[] {1, 2})]
        [TestCase("5",new[] {0, 2, 3, 4, 5})]
        public void validatorTestExpectedPasses(String option, int[] acceptedValues)
        {
            var result = SharedLib.validateOption(option, acceptedValues);
            Assert.IsTrue(result);
        }

        [Test]
        [TestCase("abcadaa", 'a', new[] {0,3,5,6})]
        [TestCase("Test String", 's', new[] {2})]
        [TestCase("Test String", 't', new[] {3,6})]
        public void charIndexInStringTestExpectedPasses(String fullText, char val, int[] expected)
        {
            var trueCount = 0;
            var results = SharedLib.getCharIndexInString(fullText, val);
            
            foreach (var r in results)
            {
                if (Array.Exists(expected, e => e == r)){
                    trueCount++;
                }
            }
            if (trueCount == results.Count){
                Assert.Pass();
            } else {
                Assert.Fail();
            }
        }
    }
}