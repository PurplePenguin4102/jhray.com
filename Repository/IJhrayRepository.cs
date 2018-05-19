using jhray.com.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jhray.com.Repository
{
    public interface IJhrayRepository
    {
        List<ChilledUser> GetAllUsers();
    }
}
