using BO.Models.Mongo;
using DAL;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    public class MongoUnitOfWorkTest
    {
        private Mock<IMongoUnitOfWork> mockDBContext;

        [SetUp]
        public void Setup()
        {
            mockDBContext = new Mock<IMongoUnitOfWork>();

            mockDBContext
                .Setup(s => s.GetAllAsync<Person>(It.IsAny<string>()))
                .ReturnsAsync(new List<Person>() { new Person() { Id = "1", FirstName = "Fix" } });
        }

        [Test, Order(1)]
        public async Task TestGetData()
        {
            var result = await mockDBContext.Object.GetAllAsync<Person>("test").ConfigureAwait(false);

            Assert.AreEqual("Fix", result.FirstOrDefault(x => x.Id == "1")?.FirstName);
        }
    }
}