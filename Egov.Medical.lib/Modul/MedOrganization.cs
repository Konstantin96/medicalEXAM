using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egov.Medical.lib.Modul
{
    public class MedOrganization
    {
        public int MedOrganizationId { get; set; }
        public string NameOfOrganization { get; set; }
        public string Address { get; set; }
        public string TelephoneNumber { get; set; }
        public List<Pacient> Pacients = new List<Pacient>();

    }
}
