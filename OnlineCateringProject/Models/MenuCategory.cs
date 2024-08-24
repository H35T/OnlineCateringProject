using System;
using System.Collections.Generic;

namespace OnlineCateringProject.Models
{
    public partial class MenuCategory
    {
        public MenuCategory()
        {
            Menus = new HashSet<Menu>();
        }

        public int CategoryId { get; set; }
        public string Category { get; set; } = null!;

        public virtual ICollection<Menu> Menus { get; set; }
    }
}
