namespace OnlineCateringProject.Models
{
    public class ProfileChangeUser
    {
        public string Name { get; set; } = null!;
        public string OldPassword { get; set; }
        public string Password { get; set; }
     
    }
}
