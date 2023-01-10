using BLL.Interfaces;
using BLL.Models;
using DataLayer.Entities;
using DataLayer.Interfaces;
using System.ComponentModel;

namespace BLL.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> AddComment(string text, int gameId, string userId)
        {
            Comment comment = new()
            {
                Body = text,
                GameId = gameId,
                UserId = userId,
                IsEdited = false,
                RepliedCommentId = 0,
                CommentedTime = DateTime.Now.ToString()
            };
            await _unitOfWork.Comments.AddAsync(comment);
            await _unitOfWork.SaveAsync();
            return gameId;
        }

        public async Task<int> AddReplyComment(string text, int commentId, string userId, int gameId)
        {
            Comment comment = new()
            {
                Body = text,
                GameId = gameId,
                UserId = userId,
                IsEdited = false,
                IsDeleted = false,
                RepliedCommentId = commentId,
                CommentedTime = DateTime.Now.ToString()
            };

            await _unitOfWork.Comments.AddAsync(comment);
            await _unitOfWork.SaveAsync();
            return gameId;
        }

        public async Task DeleteCommentAsync(int commentId)
        {
            var comment = await _unitOfWork.Comments.GetByIdAsync(commentId);
            comment.IsDeleted = true;
            comment = _unitOfWork.Comments.Update(comment);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteGeneralAsync(int commentId)
        {
            await _unitOfWork.Comments.DeleteByIdAsync(commentId);
            await _unitOfWork.SaveAsync();
            var replies = (await _unitOfWork.Comments.GetAllAsync()).Where(c => c.RepliedCommentId == commentId);
            foreach (var reply in replies)
            {
                await _unitOfWork.Comments.DeleteByIdAsync(reply.Id);
                await _unitOfWork.SaveAsync();
            }
        }

        public async Task<int> EditCommentAsync(string text, int commentId)
        {
            var comment = await _unitOfWork.Comments.GetByIdAsync(commentId);
            comment.Body = text;
            comment.IsEdited = true;
            comment.CommentedTime = DateTime.Now.ToString();
            _unitOfWork.Comments.Update(comment);
            await _unitOfWork.SaveAsync();
            return commentId;
        }

        public async Task<IEnumerable<CommentModel>> GetGameComments(int gameId, IEnumerable<UserModel> users)
        {
            var list = await _unitOfWork.Comments.GetAllAsync();
            List<CommentModel> comments = new List<CommentModel>();

            foreach (var comment in list.Where(g => g.GameId == gameId && g.RepliedCommentId == 0)
                                        .OrderByDescending(i => i.CommentedTime))
            {
                var user = users.FirstOrDefault(i => i.Id == comment.UserId);
                var replies = await GetRepliedCommentsByCommentIdAsync(comment.Id, users);

                var model = new CommentModel()
                {
                    Id = comment.Id,
                    GameId = comment.GameId,
                    Body = comment.Body,
                    CommentedTime = comment.CommentedTime.ToString(),
                    IsEdited = comment.IsEdited,
                    IsDeleted = comment.IsDeleted,
                    User = user,
                    RepliedComments = replies
                };

                comments.Add(model);
            }

            return comments;
        }

        public async Task RestoreCommentAsync(int commentId)
        {
            var comment = await _unitOfWork.Comments.GetByIdAsync(commentId);
            comment.IsDeleted = false;
            comment = _unitOfWork.Comments.Update(comment);
            await _unitOfWork.SaveAsync();
        }

        private async Task<IEnumerable<CommentModel>> GetRepliedCommentsByCommentIdAsync(int commentId, IEnumerable<UserModel> users)
        {
            var list = await _unitOfWork.Comments.GetAllAsync();
            return list.Where(i => i.RepliedCommentId == commentId)
                       .OrderByDescending(i => i.CommentedTime)
                       .Select(c =>
                       {
                           var user = users.FirstOrDefault(i => i.Id == c.UserId);
                           return new CommentModel()
                           {
                               Id = c.Id,
                               Body = c.Body,
                               CommentedTime = c.CommentedTime,
                               IsEdited = c.IsEdited,
                               User = user
                           };
                       });
        }
    }
}
