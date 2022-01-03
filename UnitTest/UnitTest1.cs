using Microsoft.VisualBasic;
using NUnit.Framework;
using SampleWebApp.Controllers;
using System.Threading.Tasks;

namespace UnitTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task  Test1()
        {
            var controller = new QuestionsController();
            var value = await controller.GetQuestion(1);
            Assert.That(value, Is.True);
        }
    }
}