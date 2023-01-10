namespace DataLayer.Entities
{
    public class Order : BaseEntity
    {
        public Order()
        {
            OrderDetails = new List<OrderDetail>();
        }
        public string? UserId { get; set; }
        public PaymentType Payment { get; set; }
        public string? Comment { get; set; }
        public decimal TotalPrice { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }

    public enum PaymentType
    {
        Cash,
        Card
    }
}
