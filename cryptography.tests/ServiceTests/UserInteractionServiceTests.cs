using System;
using cryptography.Services;
using NUnit.Framework;

namespace cryptography.tests.ServiceTests
{
    public class UserInteractionServiceTests
    {
        
        [Test]
        [TestCase("hi",new[] {1, 2})]
        [TestCase("ten",new[] {1, 2})]
        [TestCase("5",new[] {1, 2})]
        [TestCase("2",new[] {1, 3, 4, 5})]
        public void validatorTestExpectedFailures(String option, int[] acceptedValues)
        {
            var result = UserInteractionService.validateOption(option, acceptedValues);
            Assert.IsFalse(result);
        }

        [Test]
        [TestCase("2",new[] {1, 2})]
        [TestCase("5",new[] {0, 2, 3, 4, 5})]
        public void validatorTestExpectedPasses(String option, int[] acceptedValues)
        {
            var result = UserInteractionService.validateOption(option, acceptedValues);
            Assert.IsTrue(result);
        }
    }
}