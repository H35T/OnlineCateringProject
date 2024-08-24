using System.ComponentModel.DataAnnotations;

namespace OnlineCateringProject.Models
{
    public class RegisterViewModel
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
        public string? Address { get; set; }
        public string? PinCode { get; set; }
        public string? Phone { get; set; }
        public string? Mobile { get; set; }
    }
}
