using System;
using System.Collections.Generic;

namespace OnlineCateringProject.Models
{
    public partial class Customer
    {
        public Customer()
        {
            CustOrders = new HashSet<CustOrder>();
            CustomerInvoices = new HashSet<CustomerInvoice>();
            FavoriteCaterers = new HashSet<FavoriteCaterer>();
        }

        public int CustomerId { get; set; }
        public string Name { get; set; } = null!;
        public string? Address { get; set; }
        public string? PinCode { get; set; }
        public string? Phone { get; set; }
        public string? Mobile { get; set; }

        public virtual ICollection<CustOrder> CustOrders { get; set; }
        public virtual ICollection<CustomerInvoice> CustomerInvoices { get; set; }
        public virtual ICollection<FavoriteCaterer> FavoriteCaterers { get; set; }
    }
}
