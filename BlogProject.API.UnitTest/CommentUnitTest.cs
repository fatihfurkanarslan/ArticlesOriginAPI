using AutoMapper;
using BlogProject.API.Controllers;
using Moq;
using Xunit;
using BusinessLayer.AbstractManager;
using BusinessLayer;

namespace BlogProject.API.UnitTest
{
    public class CommentUnitTest
    {

        private readonly Mock<CommentManager> mockManager;
        private readonly CommentController commentController;
        private readonly Mock<IMapper> mapper;

        public CommentUnitTest()
        {
            mockManager = new Mock<CommentManager>();
            mapper = new Mock<IMapper>();
            
            commentController = new CommentController(mockManager.Object, mapper.Object);


        }


        [Fact]
        public void Test1()
        {
            var result = commentController.GetComments(2345);
            Assert.NotNull(result);

        }
    }
}