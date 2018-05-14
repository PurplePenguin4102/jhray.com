using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace jhray.com.Database.Entities
{
    public class User : EntityBase
    {
        [Key]
        public int Id{ get; set; }

        private string _name;
        [Display]
        [Editable(true)]
        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                if (_password != value)
                {
                    _password = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private DateTimeOffset _joined;
        public DateTimeOffset Joined
        {
            get => _joined;
            set
            {
                if (_joined != value)
                {
                    _joined = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public IEnumerable<UserRole> UserRoles;
    }
}
