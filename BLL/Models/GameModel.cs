using DataLayer.Entities;
using System.ComponentModel.DataAnnotations;

namespace BLL.Models
{
    public class GameModel
    {
        public GameModel()
        {
            Categories = new List<GameCategoryModel>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string? ImagePath { get; set; }
        public bool IsDeleted { get; set; } = false;
        public int Sold { get; set; }
        public List<GameCategoryModel>? Categories { get; set; }
    }
}
