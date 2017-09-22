using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MvcPL.Models
{
    public class UserViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        [Display(Name="User name"), Required(ErrorMessage = "Login field is required")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Password field is required"), DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Email field is required"),
            RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Incorrect email. Try again")]
        public string Email { get; set; }
        [Display(Name="First name")]
        public string FirstName { get; set; }
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        public byte[] Image { get; set; }

        public int? BlogId { get; set; }
    }
}