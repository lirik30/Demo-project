using System.ComponentModel.DataAnnotations;

namespace MvcPL.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        [Display(Name="User name")]
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        [Display(Name="First name")]
        public string FirstName { get; set; }
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        public byte[] Image { get; set; }

        public int? BlogId { get; set; }
    }
}