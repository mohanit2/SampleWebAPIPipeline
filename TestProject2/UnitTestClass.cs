using Moq;
using NUnit.Framework;
using System.Data.Entity;
using SampleWebApp;
using SampleWebApp.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;

namespace SampleWebAppSearchUnitTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetAllQuestions()
        {
            var data = new List<Question>
            {
                new Question { QuestionID = 1, QuestionDescription= "AAA", Question1= "testQuestion1" },
                new Question { QuestionID = 2, QuestionDescription= "BBB", Question1= "testQuestion2" },
                new Question { QuestionID = 3, QuestionDescription= "CCC", Question1= "testQuestion3" },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Question>>();
            mockSet.As<IQueryable<Question>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Question>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Question>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            mockSet.As<IQueryable<Question>>().Setup(m => m.Provider).Returns(data.Provider);

            var mockContext = new Mock<SampleDatabaseEntities>();
            mockContext.Setup(c => c.Questions).Returns(mockSet.Object);

            var service = new QuestionsController(mockContext.Object);
            var questionList = service.GetQuestions();

            Assert.AreEqual(3, questionList.Count());
           
        }

        [Test]
        public async Task GetAllQuestionsAsync()
        {

            var data = new List<Question>
            {
                new Question { QuestionID = 1, QuestionDescription= "AAA", Question1= "testQuestion1" },
                new Question { QuestionID = 2, QuestionDescription= "BBB", Question1= "testQuestion2" },
                new Question { QuestionID = 3, QuestionDescription= "CCC", Question1= "testQuestion3" },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Question>>();
            mockSet.As<IDbAsyncEnumerable<Question>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<Question>(data.GetEnumerator()));

            mockSet.As<IQueryable<Question>>()
                .Setup(m => m.Provider)
                .Returns(new AsyncUnitClass<Question>(data.Provider));

            mockSet.As<IQueryable<Question>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Question>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Question>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<SampleDatabaseEntities>();
            mockContext.Setup(c => c.Questions).Returns(mockSet.Object);

            var service = new QuestionsController(mockContext.Object);
            var questionList = await service.GetAllQuestionsAsync();

            Assert.AreEqual(3, questionList.Count);
            Assert.AreEqual("AAA", questionList[0].QuestionDescription);
            Assert.AreEqual("BBB", questionList[1].QuestionDescription);
            Assert.AreEqual("CCC", questionList[2].QuestionDescription);
        }
    }
}