using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using SampleWebApp.Controllers;
using System;
using System.Threading.Tasks;
using Assert = NUnit.Framework.Assert;

namespace SampleWebSearchUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [Test]
        public async Task TestMethod1()
        {
            var eightBall = new QuestionsController();
            var answer = await eightBall.GetQuestion(1);


            Assert.That(answer, Is.True);
        }
    }
}
