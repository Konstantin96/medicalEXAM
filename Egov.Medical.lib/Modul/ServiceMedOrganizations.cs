using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egov.Medical.lib.Modul
{
    public class ServiceMedOrganizations
    {
        public static List<MedOrganization> GetMedOrganization()
        {
            using (var db = new LiteDatabase(@"EgovMedDB.db"))
            {
                List<MedOrganization> ogs= db.GetCollection<MedOrganization>("MedOrganization").FindAll().ToList();
                for (int i = 0; i < ogs.Count; i++)
                {
                    ogs[i].Pacients= db.GetCollection<Pacient>("Pacient").FindAll().Where(w=>w.MedOrganizationId == ogs[i].MedOrganizationId).ToList();
                }
                return ogs;
            }
        }
        public static bool AddMedOrganization(MedOrganization org)
        {
            try
            {
                using (var db = new LiteDatabase(@"EgovMedDB.db"))
                {
                    var collection = db.GetCollection<MedOrganization>("MedOrganization");
                    collection.Insert(org);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
