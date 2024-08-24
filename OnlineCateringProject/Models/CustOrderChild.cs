using System;
using System.Collections.Generic;

namespace OnlineCateringProject.Models
{
    public partial class CustOrderChild
    {
        public int OrderNo { get; set; }
        public int MenuItemNo { get; set; }
        public int? Quantity { get; set; }

        public virtual Menu MenuItemNoNavigation { get; set; } = null!;
        public virtual CustOrder OrderNoNavigation { get; set; } = null!;
    }
}
