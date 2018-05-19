using jhray.com.Database.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace jhray.com.Models.AccountViewModels
{
    public class ManageUsersViewModel
    {
        public List<ChilledUser> Users { get; set; }
        public List<ChilledUser> Roles { get; set; }
    }
}
