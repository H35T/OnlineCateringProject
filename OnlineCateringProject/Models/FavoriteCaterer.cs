using System;
using System.Collections.Generic;

namespace OnlineCateringProject.Models
{
    public partial class FavoriteCaterer
    {
        public int FavoriteId { get; set; }
        public int? CustomerId { get; set; }
        public int? CatererId { get; set; }

        public virtual Caterer? Caterer { get; set; }
        public virtual Customer? Customer { get; set; }
    }
}
