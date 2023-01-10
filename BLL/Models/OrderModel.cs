using DataLayer.Entities;

namespace BLL.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public PaymentType Payment { get; set; }
        public string? Comment { get; set; }
        public decimal TotalPrice { get; set; }
        public ICollection<int> OrderDetailIds { get; set; }
    }
}
