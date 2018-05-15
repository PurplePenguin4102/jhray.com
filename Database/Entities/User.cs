using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace jhray.com.Database.Entities
{
    public class User : IdentityUser, INotifyPropertyChanged
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
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        public IEnumerable<UserRole> UserRoles;
    }
}
