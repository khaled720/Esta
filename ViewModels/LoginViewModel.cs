﻿using System.ComponentModel.DataAnnotations;

namespace ESTA.ViewModels
{
    public class LoginViewModel
    {

        [Required]
        public string Email { get; set; }
       [Required]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
