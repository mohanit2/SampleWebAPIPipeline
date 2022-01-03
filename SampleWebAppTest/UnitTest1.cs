using NUnit.Framework;
using SampleWebApp;
using SampleWebApp.Controllers;
using System.Threading.Tasks;
using System.Web.Http;

namespace SampleWebAppTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetQuestionsAsync_ShouldReturnCorrect()
        {
            var eightBall = new QuestionsController();
            var answer = await eightBall.GetQuestion(1);
            

            Assert.That(answer, Is.True);
        }

        //private List<Question> GetTestProducts()
        //{
        //    var testProducts = null;
        //    testProducts.Add(new Question { QuestionID = 1, Question1 = "Demo1", QuestionDescription = 1 });
        //    //testProducts.Add(new Product { Id = 2, Name = "Demo2", Price = 3.75M });
        //    //testProducts.Add(new Product { Id = 3, Name = "Demo3", Price = 16.99M });
        //    //testProducts.Add(new Product { Id = 4, Name = "Demo4", Price = 11.00M });

        //    return testProducts;
        //}
    }
}