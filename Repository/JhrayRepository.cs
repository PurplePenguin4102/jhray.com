using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jhray.com.Database;
using jhray.com.Database.Entities;

namespace jhray.com.Repository
{
    public class JhrayRepository : IJhrayRepository
    {
        private static JhrayRepository _instance;
        public static JhrayRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new JhrayRepository();
                }
                return _instance;
            }    
        }

        //private ChilledDbContext ctx;
        private JhrayRepository()
        {
            //ctx = new ChilledDbContext();
        }

        public List<ChilledUser> GetAllUsers()
        {
            using (var context = new ChilledDbContext())
            {
                
            }
            throw new NotImplementedException();
        }
    }
}
