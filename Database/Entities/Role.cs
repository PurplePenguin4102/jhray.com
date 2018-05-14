using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace jhray.com.Database.Entities
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public string RoleName { get; set; }

        public IEnumerable<UserRole> UserRoles { get; set; }
    }
}
