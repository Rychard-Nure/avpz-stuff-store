using BLL.Models;
using BLL.Services;
using DataLayer.Entities;
using DataLayer.Interfaces;
using Moq;

namespace GameStore.Test.BLL
{
    internal class CommentServiceTest
    {
        [TestCase("comment1", 1, "id1")]
        [TestCase("comment2", 2, "id2")]
        public async Task CommentService_AddComment_ReturnsGameId(string text, int gameId, string userId)
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork
                .Setup(m => m.Comments.AddAsync(It.IsAny<Comment>()));

            var commentService = new CommentService(mockUnitOfWork.Object);
            var res = await commentService.AddComment(text, gameId, userId);
            Assert.That(gameId, Is.EqualTo(res));
        }

        [TestCase("reply1", 1, "id1", 1)]
        [TestCase("reply2", 2, "id2", 2)]
        public async Task CommentService_AddReplyComment_ReturnsGameId(string text, int commentId, string userId, int gameId)
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork
                .Setup(m => m.Comments.AddAsync(It.IsAny<Comment>()));

            var commentService = new CommentService(mockUnitOfWork.Object);
            var res = await commentService.AddReplyComment(text, commentId, userId, gameId);
            Assert.That(gameId, Is.EqualTo(res));
        }

        [TestCase(1)]
        public async Task CommentService_DeleteComment(int commentId)
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(m => m.Comments.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(UnitTestHelper.comments[commentId-1]);

            var commentService = new CommentService(mockUnitOfWork.Object);

            //actual
            await commentService.DeleteCommentAsync(commentId);

            //assert
            var comment = UnitTestHelper.comments[0];
            comment.IsDeleted = true;

            mockUnitOfWork.Verify(x => x.Comments.Update(comment), Times.Once());
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once());

        }

        [TestCase(1)]
        [TestCase(2)]
        public async Task CommentService_DeleteGeneralComment(int commentId)
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(m => m.Comments.DeleteByIdAsync(It.IsAny<int>()));

            var commentService = new CommentService(mockUnitOfWork.Object);

            //actual
            await commentService.DeleteGeneralAsync(commentId);

            mockUnitOfWork.Verify(x => x.Comments.DeleteByIdAsync(commentId), Times.Once());
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once());

        }

        [Test]
        public async Task CommentService_UpdateComment()
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(m => m.Comments.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(UnitTestHelper.comments.First());
            mockUnitOfWork.Setup(m => m.Comments.Update(It.IsAny<Comment>()));

            var commentService = new CommentService(mockUnitOfWork.Object);
            var comment = UnitTestHelper.comments.First();

            //actual
            await commentService.EditCommentAsync(comment.Body, comment.Id);

            //assert
            mockUnitOfWork.Verify(x => x.Comments.Update(It.Is<Comment>(x => 
                x.Id == comment.Id && x.IsEdited == comment.IsEdited &&
                x.IsDeleted == comment.IsDeleted && x.Body == comment.Body &&
                x.UserId == comment.UserId && x.CommentedTime == comment.CommentedTime &&
                x.RepliedCommentId == comment.RepliedCommentId)), Times.Once());

            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once());
        }

        [TestCase(1)]
        public async Task CommentService_RestoreDeletedComment(int commentId)
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(m => m.Comments.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(UnitTestHelper.comments[commentId - 1]);

            var commentService = new CommentService(mockUnitOfWork.Object);

            //actual
            await commentService.RestoreCommentAsync(commentId);

            //assert
            var comment = UnitTestHelper.comments[0];
            comment.IsDeleted = false;

            mockUnitOfWork.Verify(x => x.Comments.Update(comment), Times.Once());
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once());

        }

        #region TestData
        public List<CommentModel> Comments =>
            new List<CommentModel>
            {
                new CommentModel()
                {
                    Id= 1,
                    Body = "body1",
                    IsDeleted= false,
                    IsEdited= false,
                    CommentedTime= DateTime.Now.ToString(),
                    GameId = 1,
                    User = new UserModel() {Id = "id1", AvatarPath = "path1", FirstName="F1", LastName = "L1"}
                },
                new CommentModel()
                {
                    Id= 2,
                    Body = "body2",
                    IsDeleted= false,
                    IsEdited= false,
                    CommentedTime= DateTime.Now.ToString(),
                    GameId = 1,
                    User = new UserModel() {Id = "id2", AvatarPath = "path2", FirstName="F2", LastName = "L2"}
                }
            };
        #endregion
    }
}
