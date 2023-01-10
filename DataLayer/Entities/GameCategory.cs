using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities
{
    public class GameCategory : BaseEntity
    {
        public GameCategory()
        {
            Games = new List<CategoryGame>();
        }

        [Required]
        [StringLength(30)]
        public string? Name { get; set; }
        public int GameId { get; set; }
        public List<CategoryGame>? Games { get; set; }
    }
}
