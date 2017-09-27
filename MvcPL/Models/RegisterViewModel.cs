using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcPL.Models
{
    public class RegisterViewModel
    {
        public int Id { get; set; }

        [Display(Name = "User name"), Required(ErrorMessage = "Login field is required")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Email is required"),
         RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Incorrect email. Try again")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password field is required"), DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password), Compare("Password", ErrorMessage = "Passwords don't match")]
        public string ConfirmPassword { get; set; }
    }
}