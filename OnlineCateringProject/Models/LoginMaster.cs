using System;
using System.Collections.Generic;

namespace OnlineCateringProject.Models
{
    public partial class LoginMaster
    {
        public int UserId { get; set; }
        public string Name { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string UserType { get; set; } = null!;
    }
}
