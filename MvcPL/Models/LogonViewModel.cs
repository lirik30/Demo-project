using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcPL.Models
{
    public class LogonViewModel
    {
        [Required(ErrorMessage = "The field cannot be empty")]
        public string Login { get; set; }

        [Required(ErrorMessage = "The field cannot be empty"), DataType(DataType.Password)]
        [Display(Name = "Enter your password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}