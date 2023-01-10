using BLL.Models;

namespace BLL.Interfaces
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentModel>> GetGameComments(int gameId, IEnumerable<UserModel> users);
        Task<int> AddComment(string text, int gameId, string userId);
        Task<int> AddReplyComment(string text, int commentId, string userId, int gameId);
        Task<int> EditCommentAsync(string text, int commentId);
        Task DeleteCommentAsync(int commentId);
        Task DeleteGeneralAsync(int commentId);
        Task RestoreCommentAsync(int commentId);
    }
}
