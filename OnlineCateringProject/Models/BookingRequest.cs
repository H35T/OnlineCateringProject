namespace OnlineCateringProject.Models
{
    public class BookingRequest
    {
        public int? CustomerId { get; set; }
        public int? CatererId { get; set; }
        public decimal? CostPerPlate { get; set; }
        public int? MinPeople { get; set; }
        public int? MaxPeople { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string? DeliveryAddress { get; set; }
       
        public List<Order>? Orders { get; set; } = new List<Order>();

    }
    public class Order
    {
        public int MenuItemNo { get; set; }
        public decimal? Price { get; set; }
        public int Quantity { get; set; }
        public string Name { get; set; }
       

    }
}
