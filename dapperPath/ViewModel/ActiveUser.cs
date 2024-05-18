using dapperPath.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dapperPath.ViewModel
{
    public class ActiveUser
    {
        private static Users _user;
        public static Users Users
        {
            get { return _user; }
            set
            {
                _user = value;
        
            }
        }
    }
}
