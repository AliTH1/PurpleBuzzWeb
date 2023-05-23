﻿using System.ComponentModel.DataAnnotations;

namespace PurpleBuzzWeb.ViewModel.Auth
{
    public class LoginVM
    {
        [Required, MaxLength(255), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required, MinLength(8), DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
