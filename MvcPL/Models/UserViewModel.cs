﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcPL.Models
{
    public class UserViewModel
    {
        //что входит в UserViewModel и как представить блоги\посты\комменты, связанные друг с другом
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