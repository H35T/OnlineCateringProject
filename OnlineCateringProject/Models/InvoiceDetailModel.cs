namespace OnlineCateringProject.Models
{

    public class InvoiceDetailModel
    {
        public InvoiceModel? Bill { get; set; }
        public OrderModel? Order { get; set; }
        public List<MenuItemModel>? ListMenu { get; set; }
    }

    public class InvoiceModel
    {
        public int? InvoiceNo { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public decimal? TotalAmount { get; set; }
    }

    public class OrderModel
    {
        public int? OrderNo { get; set; }
        public DateTime? OrderDate { get; set; }
        public int? CatererId { get; set; }
        public int? CustomerId { get; set; }
        public string? DeliveryAddress { get; set; }
        public int? MaxPeople { get; set; }
        public int? MinPeople { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string? OrderStatus { get; set; }
    }

    public class MenuItemModel
    {
        public int? MenuItemNo { get; set; }
        public string? ItemName { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
    }


}
