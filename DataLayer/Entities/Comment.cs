using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities
{
    public class Comment : BaseEntity
    {
        [Required]
        public string Body { get; set; } = string.Empty;
        public bool IsEdited { get; set; }
        public bool IsDeleted { get; set; }
        public string CommentedTime { get; set; }
        public int GameId { get; set; }
        public string UserId { get; set; }
        public int RepliedCommentId { get; set; } = 0;
    }
}
