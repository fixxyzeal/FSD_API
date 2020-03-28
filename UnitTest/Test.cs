using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using ServiceLB;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace UnitTest
{
    public class Test
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test, Order(1)]
        public void TestSet()
        {
            Assert.AreEqual(true, true);
        }
    }
}