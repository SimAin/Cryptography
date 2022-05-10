using System;
using cryptography.Services;
using NUnit.Framework;

namespace cryptography.tests.ServiceTests
{
    public class StringOperationsServiceTests
    {
        [Test]
        [TestCase("abcadaa", 'a', new[] {0,3,5,6})]
        [TestCase("Test String", 's', new[] {2})]
        [TestCase("Test String", 't', new[] {3,6})]
        public void charIndexInStringTestExpectedPasses(String fullText, char val, int[] expected)
        {
            var trueCount = 0;
            var results = StringOperationsService.getCharIndexInString(fullText, val);
            
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