using Egov.Medical.lib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneratorName;

namespace Egov.Medical.lib.Modul
{
    public class User : IUser, IPerson
    {
        public DateTime CreateDate { get; set; }
        public DateTime DoB { get; set; }
        public string FIO { get; set; }
        public string IIN { get; set; }
        public bool IsBlock { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
        public Gender Sex { get; set; }
        public int UserId { get; set; }
        public string WhoCreated { get; set; }
        public int Age()
        {
            return (DateTime.Now.Year-DoB.Year);
        }

        public void BlockedUser(bool status)
        {
            //fdf
        }
    }
}
