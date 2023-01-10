namespace DataLayer.Entities
{
    public class OrderDetail : BaseEntity
    {
        public int GameId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int OrderId { get; set; }
    }
}