using Egov.Medical.lib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneratorName;
using Egov.Medical.lib.Modul;

namespace Egov.Medical.lib.Modul
{
    public class Pacient : IPerson
    {
        public int PacientID { get; set; }
        public DateTime DoB { get; set; }

        public string FIO { get; set; }

        public string IIN { get; set; }

        public Gender Sex { get; set; }

        public int UserId { get; set; }

        public int Age()
        {
            return (DateTime.Now.Year - DoB.Year);
        }
        public int MedOrganizationId { get; set; } = 0;
        public MedOrganization MedOrganization { get; set; }

    }
}
