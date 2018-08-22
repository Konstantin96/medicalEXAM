using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneratorName;

namespace Egov.Medical.lib.Interfaces
{
    public interface IPerson
    {
        int UserId { get; set; }
        string FIO { get; set; }
        int Age();
        DateTime DoB { get; set; }
        Gender Sex { get; set; }
        string IIN { get; set; }
        
    }
}
