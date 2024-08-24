using System;
using System.Collections.Generic;

namespace OnlineCateringProject.Models
{
    public partial class Menu
    {
        public Menu()
        {
            CustOrderChildren = new HashSet<CustOrderChild>();
        }

        public int MenuItemNo { get; set; }
        public string ItemName { get; set; } = null!;
        public decimal? Price { get; set; }
        public int? CategoryId { get; set; }
        public int? CatererId { get; set; }
        public string? Describe { get; set; }

        public virtual MenuCategory? Category { get; set; }
        public virtual Caterer? Caterer { get; set; }
        public virtual ICollection<CustOrderChild> CustOrderChildren { get; set; }
    }
}
