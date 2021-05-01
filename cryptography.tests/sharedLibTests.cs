using NUnit.Framework;
using cryptography;

namespace cryptography.Tests
{
    [TestFixture]
    public class sharedLibTests
    {
        private sharedLib _sharedLib;

        [SetUp]
        public void Setup()
        {
            _sharedLib = new sharedLib();
        }

        [Test]
        public void validatorTestNonIntFails()
        {
            var result = sharedLib.validateOption("hi", new int[] {1, 2});
            Assert.IsFalse(result);
        }

        [Test]
        public void validatorTestUnacceptedIntFails()
        {
            var result = sharedLib.validateOption("5", new int[] {1, 2});
            Assert.IsFalse(result);
        }

        [Test]
        public void validatorTestValidIntPasses()
        {
            var result = sharedLib.validateOption("2", new int[] {1, 2});
            Assert.IsTrue(result);
        }
    }
}