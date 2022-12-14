using NUnit.Framework;
using System.Diagnostics.CodeAnalysis;

namespace com.TechKart.UserApi.UnitTests
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class Tests
    {

        [SetUp]
        public void Setup()
        {
        }
        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}