using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace jhray.com.Models.GateKeeperViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember the dawn?")]
        public bool RememberMe { get; set; }
    }
}
