using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities
{
    public class Game : BaseEntity
    {
        public Game()
        {
            Categories = new List<CategoryGame>();
        }

        [Required]
        [StringLength(100)]
        public string? Name { get; set; }

        [Required]
        [MaxLength(10)]
        public decimal Price { get; set; } = 0;

        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        public string? ImagePath { get; set; }

        [Required]
        public bool IsDeleted { get; set; } = false;
        public int Sold { get; set; } = 0;

        public int GameCategoryId { get; set; }

        public List<CategoryGame>? Categories { get; set; }
    }
}
