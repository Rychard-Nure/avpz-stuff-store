using DataLayer.Data;
using DataLayer.Repositories;
namespace GameStore.Test.DataLayer
{
    [TestFixture]
    public class CommentRepositoryTest
    {
        [Test]
        public async Task CommentRepository_GetAllAsync_ReturnAllValues()
        {
            using var context = new GameStoreDBContext(UnitTestHelper.GetUnitTestDbOptions());

            var commentRepository = new CommentRepository(context);
            var comments = await commentRepository.GetAllAsync();

            Assert.That(comments, Is.EqualTo(UnitTestHelper.comments).Using(new CommentEqualityComparer()));
        }

        [TestCase(1)]
        [TestCase(2)]
        public async Task CommentRepository_GetByIdAsync_ReturnSingleComment(int id)
        {
            using var context = new GameStoreDBContext(UnitTestHelper.GetUnitTestDbOptions());

            var commentRepository = new CommentRepository(context);
            var comment = await commentRepository.GetByIdAsync(id);
            var expectedComment = UnitTestHelper.comments.FirstOrDefault(i => i.Id == id);

            Assert.That(comment, Is.EqualTo(expectedComment).Using(new CommentEqualityComparer()));
        }

        [Test]
        public async Task CommentRepository_AddAsync_AddCommentToDatabase()
        {
            using var context = new GameStoreDBContext(UnitTestHelper.GetUnitTestDbOptions());

            var commentRepository = new CommentRepository(context);
            var comment = await commentRepository.GetByIdAsync(1);
            comment.Id = 4;
            await commentRepository.AddAsync(comment);
            await context.SaveChangesAsync();

            Assert.That(context.Comments.Count(), Is.EqualTo(4));
        }

        [TestCase(1)]
        [TestCase(2)]
        public async Task CommentRepository_DeleteByIdAsync_DeleteComment(int id)
        {
            using var context = new GameStoreDBContext(UnitTestHelper.GetUnitTestDbOptions());

            var commentRepository = new CommentRepository(context);
            await commentRepository.DeleteByIdAsync(id);
            await context.SaveChangesAsync();

            Assert.That(context.Comments.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task CommentRepository_DeleteAsync_DeleteComment()
        {
            using var context = new GameStoreDBContext(UnitTestHelper.GetUnitTestDbOptions());

            var commentRepository = new CommentRepository(context);
            var comment = await commentRepository.GetByIdAsync(1);
            commentRepository.Delete(comment);
            await context.SaveChangesAsync();

            Assert.That(context.Comments.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task CommentRepository_Update_UpdateComment()
        {
            using var context = new GameStoreDBContext(UnitTestHelper.GetUnitTestDbOptions());

            var commentRepository = new CommentRepository(context);
            var comment = await commentRepository.GetByIdAsync(1);
            comment.Body = "Updated";
            var result = commentRepository.Update(comment);
            await context.SaveChangesAsync();

            Assert.That(result, Is.EqualTo(comment));
        }
    }
}
