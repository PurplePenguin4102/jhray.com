using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace jhray.com.Models.GemMasterViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Thou Name, Hero")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Thy Email Address, Hero")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Thy Secret Spell, Hero")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Spell")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
