using System;
using System.Collections.Generic;

namespace OnlineCateringProject.Models
{
    public partial class CustOrder
    {
        public CustOrder()
        {
            CustOrderChildren = new HashSet<CustOrderChild>();
            CustomerInvoices = new HashSet<CustomerInvoice>();
        }

        public int OrderNo { get; set; }
        public DateTime OrderDate { get; set; }
        public int? CustomerId { get; set; }
        public int? CatererId { get; set; }
        public decimal? CostPerPlate { get; set; }
        public int? MinPeople { get; set; }
        public int? MaxPeople { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string? DeliveryAddress { get; set; }
        public string OrderStatus { get; set; } = null!;

        public virtual Caterer? Caterer { get; set; }
        public virtual Customer? Customer { get; set; }
        public virtual ICollection<CustOrderChild> CustOrderChildren { get; set; }
        public virtual ICollection<CustomerInvoice> CustomerInvoices { get; set; }
    }
}
