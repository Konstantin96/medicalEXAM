using Egov.Medical.lib.Modul;
using EGOV.medical.Modul;
using GeneratorName;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGOV.medical
{
    public enum TypeMenu { type1, type2 }
    class Program
    {
        static void Main(string[] args)
        {

            ServiceProgram.Menu(TypeMenu.type1);
            int choise = Int32.Parse(Console.ReadLine());
            if (choise == 1)
            {
                ServiceProgram.Autorization();
            }
            else if (choise == 2)
            {
                Console.Clear();
                if (ServiceUser.Registration(ServiceProgram.GetUserInfoRegistration()))
                {
                    Console.WriteLine("Register OK");
                }
                else
                {
                    Console.WriteLine("Register ERROR");
                }
            }
        }
        
    }
}
