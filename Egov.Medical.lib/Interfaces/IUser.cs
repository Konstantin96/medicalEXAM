using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egov.Medical.lib.Interfaces
{
    interface IUser
    {
       
        string Login { get; set; }
        string Password { get; set; }
        int Role { get; set; }
        DateTime CreateDate { get; set; }
        string WhoCreated { get; set; }
        bool IsBlock { get; set; }
        void BlockedUser(bool status);
    }
}
