using AutoMapper;
using BlogProject.API.Controllers;
using Moq;
using Xunit;
using BusinessLayer.AbstractManager;

namespace BlogProject.API.UnitTest
{
    public class CommentUnitTest
    {

        private readonly Mock<ICommentManager> mockManager;
        private readonly CommentController commentController;
        private readonly Mock<IMapper> mapper;

        public CommentUnitTest()
        {
            mockManager = new Mock<ICommentManager>();
            mapper = new Mock<IMapper>();
            
            commentController = new CommentController(mockManager.Object, mapper.Object);


        }


        [Fact]
        public void Test1()
        {
            var result = commentController.GetComments();
            Assert.NotNull(result);

        }
    }
}