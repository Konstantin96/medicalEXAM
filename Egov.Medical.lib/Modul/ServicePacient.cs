using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Egov.Medical.lib.Modul
{
    public class ServicePacient
    {
        public static List<Pacient> GetPacient()
        {
            XmlDocument pacient = new XmlDocument();
            XmlElement root = pacient.CreateElement("pacient");

           

            using (var db = new LiteDatabase(@"EgovMedDB.db"))
            {
                return db.GetCollection<Pacient>("Pacient").FindAll().ToList();
            }
        }
        public static bool AddPacient(Pacient Pacient)
        {
            try
            {
                using (var db = new LiteDatabase(@"EgovMedDB.db"))
                {
                    var collection = db.GetCollection<Pacient>("Pacient");
                    collection.Insert(Pacient);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static void AddMedOrganizationToPAcient(int userID,int MedOrganization)
        {
            using(var db=new LiteDatabase(@"EgovMedDB.db"))
            {
                var collection = db.GetCollection<Pacient>("Pacient");
                Pacient pac = collection.FindOne(f => f.PacientID == userID);
                pac.MedOrganizationId = MedOrganization;
                collection.Update(pac);
            }
        }

    }
}
