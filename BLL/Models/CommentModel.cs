namespace BLL.Models
{
    public class CommentModel
    {
        public CommentModel()
        {
            RepliedComments = new List<CommentModel>();
        }
        public int Id { get; set; }
        public int GameId { get; set; }
        public string Body { get; set; } = string.Empty;
        public bool IsEdited { get; set; }
        public bool IsDeleted { get; set; }
        public string CommentedTime { get; set; }
        public UserModel User { get; set; }
        public IEnumerable<CommentModel> RepliedComments { get; set; }
    }
}
