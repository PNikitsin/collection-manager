﻿using System.ComponentModel.DataAnnotations;

namespace Collections.Web.ViewModels.Admin
{
    public class CreateUserViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}