using System;
using System.Collections.Generic;

namespace OnlineCateringProject.Models
{
    public partial class Caterer
    {
        public Caterer()
        {
            CustOrders = new HashSet<CustOrder>();
            FavoriteCaterers = new HashSet<FavoriteCaterer>();
            Menus = new HashSet<Menu>();
        }

        public int CatererId { get; set; }
        public string Name { get; set; } = null!;
        public string? Address { get; set; }
        public string? PinCode { get; set; }
        public string? Phone { get; set; }
        public string? Mobile { get; set; }

        public virtual ICollection<CustOrder> CustOrders { get; set; }
        public virtual ICollection<FavoriteCaterer> FavoriteCaterers { get; set; }
        public virtual ICollection<Menu> Menus { get; set; }
    }
}
