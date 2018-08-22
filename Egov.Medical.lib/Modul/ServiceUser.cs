using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Egov.Medical.lib.Modul
{
    public enum StatusOfAutorization
    {
        status01, status02, status03
    }
    public class ServiceUser
    {
        public static bool Registration(User user)
        {
            try
            {
                using (var db = new LiteDatabase(@"EgovMedDB.db"))
                {
                    LiteCollection<User> users = db.GetCollection<User>("User");
                    users.Insert(user);
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.Gray;
                return false;
            }
        }
        public static bool UserIsExist(string login)
        {
            using (var db = new LiteDatabase(@"EgovMedDB.db"))
            {
                LiteCollection<User> users = db.GetCollection<User>("User");
                User newUser = users.FindOne(f => f.Login == login);
                if (newUser != null)
                    return true;
                else
                    return false;
            }
        }
        public static StatusOfAutorization LoginOn(string login, string password, out User newUser)
        {
            newUser = null;
            try
            {
                using (var db = new LiteDatabase(@"EgovMedDB.db"))
                {
                    LiteCollection<User> users = db.GetCollection<User>("User");
                    newUser = users.FindOne(f => f.Login == login && f.Password == password);
                    if (newUser != null)
                        return StatusOfAutorization.status01;
                    else
                        return StatusOfAutorization.status02;
                }
            }
            catch (Exception ex)
            {
                return StatusOfAutorization.status03;
            }
        }
        public static void BlockUser(int UserId)
        {
            try
            {
                using (var db = new LiteDatabase(@"EgovMedDB.db"))
                {
                    LiteCollection<User> users = db.GetCollection<User>("User");
                    User newUser = users.FindOne(f => f.UserId == UserId);
                    newUser.IsBlock = true;
                    users.Update(newUser);
                }
            }
            catch (Exception ex)
            {

            }
        }

        public static void BlockUser(string login)
        {
            try
            {
                using (var db = new LiteDatabase(@"EgovMedDB.db"))
                {
                    LiteCollection<User> users = db.GetCollection<User>("User");

                    User newUser = users.FindOne(f => f.Login == login);
                    newUser.IsBlock = true;

                    users.Update(newUser);
                }
            }
            catch (Exception)
            {

            }
        }
    }
}

