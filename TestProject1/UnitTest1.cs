using Microsoft.AspNetCore.Mvc;
using Moq;
using SampleWebApp;
using SampleWebApp.Controllers;
using System.Data.Entity;
using System.Linq;
using Xunit;

namespace TestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void Task_GetPostById_Return_OkResult()
        {
            var mockSet = new Mock<DbSet<Question>>();

            var mockContext = new Mock<SampleDatabaseEntities>();
            mockContext.Setup(m => m.Questions).Returns(mockSet.Object);
            //Arrange  
            var controller = new SampleDatabaseEntities();
            var blogs = controller.Questions.All(mockContext.);
          //  Assert();

            // var data = await controller.GetQuestion(postId);

            //Assert  
            Assert.NotEmpty(blogs); //IsType<OkObjectResult>(data);
        }
    }
}