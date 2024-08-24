using System;
using System.Collections.Generic;

namespace OnlineCateringProject.Models
{
    public partial class CustomerInvoice
    {
        public int InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public int? OrderNo { get; set; }
        public int? CustomerId { get; set; }
        public decimal? TotalAmount { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual CustOrder? OrderNoNavigation { get; set; }
    }
}
