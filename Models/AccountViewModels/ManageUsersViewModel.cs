using jhray.com.Database.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace jhray.com.Models.AccountViewModels
{
    public class ManageUsersViewModel
    {
        public List<ChilledUser> Users { get; set; }
        [Display(Name="New Role")]
        public List<IdentityRole> Roles { get; set; }
        public Dictionary<ChilledUser, IEnumerable<string>> UserRoles { get; set; }

        
        [Display(Name ="New Role")]
        public string NewRole { get; set; }
    }
}
