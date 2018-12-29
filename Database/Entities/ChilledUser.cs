using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Identity;

namespace jhray.com.Database.Entities
{
    public class ChilledUser : IdentityUser, INotifyPropertyChanged
    {
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

        public List<Gem> CreatedGems { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //public IEnumerable<UserRole> UserRoles;
    }
}
