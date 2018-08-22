using Egov.Medical.lib.Modul;
using GeneratorName;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace EGOV.medical.Modul
{
    class ServiceProgram
    {
        public ServiceProgram()
        {

        }
        public ServiceProgram(User u)
        {
            user = u;
        }
        private static User user;
        public static void Menu(TypeMenu typemenu)
        {
            switch (typemenu)
            {
                case TypeMenu.type1:
                    {
                        Console.Write("1.Войти\n2.Регистрация: ");
                    }
                    break;
                case TypeMenu.type2:
                    {
                        Console.WriteLine("1.Список организаций");
                        Console.WriteLine("2.Добавить организацию");
                        Console.WriteLine("-----------------------");
                        Console.WriteLine("3.Список пациентов");
                        Console.Write("4.Добавить пациента: ");
                    }
                    break;
                default:
                    break;
            }


        }
        public static int GetpunktMenu()
        {
            return Int32.Parse(Console.ReadLine());
        }
        public static User GetUserInfoRegistration()
        {
            User user = new User();
            Console.Write("{0,-40}", "Введите ФИО: ");
            user.FIO = Console.ReadLine();
            Console.Write("{0,-40}", "Введите ИИН: ");
            user.IIN = Console.ReadLine();
            Console.Write("{0,-40}", "Введите дату рождения: ");
            user.DoB = DateTime.Parse(Console.ReadLine());
            Console.Write("{0,-40}", "Введите пол(0-ж,1=м): ");
            user.Sex = (Gender)Int32.Parse(Console.ReadLine());
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.Write("{0,-40}", "Введите Login: ");
            user.Login = Console.ReadLine();
            Console.Write("{0,-40}", "Введите Password: ");
            user.Password = Console.ReadLine();
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            user.CreateDate = DateTime.Now;
            user.IsBlock = false;

            return user;
        }
        public static void Autorization()
        {
            int count = 0;

            do
            {
                user = new User();
                Console.Write("Введите логин: ");
                user.Login = Console.ReadLine();
                Console.Write("Введите пароль: ");
                user.Password = Console.ReadLine();


                if (ServiceUser.UserIsExist(user.Login))
                {
                    StatusOfAutorization status = ServiceUser.LoginOn(user.Login, user.Password, out user);
                    if (status == StatusOfAutorization.status02)
                    {
                        count++;
                        Console.WriteLine("У вас осталось {0} попыток", 3 - count);
                    }
                    else if (status == StatusOfAutorization.status01)
                    {
                        do
                        {
                            Console.Clear();
                            SetConsoleColor(string.Format("Добро пожаловать, {0}", user.FIO), ConsoleColor.Green);
                            Menu(TypeMenu.type2);
                            switch (GetpunktMenu())
                            {
                                case 1:
                                    {
                                        PrintMedOrganization();
                                    }
                                    break;
                                case 2:
                                    {
                                        AddMedOrganization();
                                    }
                                    break;
                                case 3:
                                    {
                                        PrintPacient();
                                    }
                                    break;
                                case 4:
                                    {
                                        AddPacient();
                                    }
                                    break;
                                default:
                                    break;
                            }
                        } while (Console.ReadLine() != "back");
                    }
                    else
                    {
                        SetConsoleColor("Ошибка авторизации", ConsoleColor.Red);
                        break;
                    }
                }
                else
                {
                    Console.Clear();
                    SetConsoleColor(string.Format("Такого логина или пароля нету"), ConsoleColor.Red);
                }

            } while (count < 3);

            if (count == 3)
            {
                user = new User();
                ServiceUser.BlockUser(user.Login);
                Console.Clear();
                SetConsoleColor(string.Format("Вы залокированы!"), ConsoleColor.Red);
            }

        }
        public static void GetAllUser()
        {
            XmlDocument xDoc = new XmlDocument();
            XmlElement root = xDoc.CreateElement("user");
            using (var db = new LiteDatabase(@"EgovMedDB.db"))
            {
                LiteCollection<User> users = db.GetCollection<User>("User");
                foreach (var item in users.FindAll())
                {
                    Console.WriteLine("FIO - {0}\tData Registration {1}", item.FIO, item.CreateDate);
                }
            }
        }
        public static void SetConsoleColor(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        public static void PrintMedOrganization()
        {
            foreach (MedOrganization item in ServiceMedOrganizations.GetMedOrganization())
            {
                Console.WriteLine("{0} ({1})", item.NameOfOrganization, item.TelephoneNumber);
            }
        }
        public static void AddMedOrganization()
        {
            MedOrganization newMedOrganization = new MedOrganization();
            Console.Write("Введите название мед организации: ");
            newMedOrganization.NameOfOrganization = Console.ReadLine();
            Console.Write("Введите адрес мед организации: ");
            newMedOrganization.Address = Console.ReadLine();
            Console.Write("Введите телефон мед организации: ");
            newMedOrganization.TelephoneNumber = Console.ReadLine();
            if (ServiceMedOrganizations.AddMedOrganization(newMedOrganization))
            {
                SetConsoleColor(string.Format("Организация {0} добавлена", newMedOrganization.NameOfOrganization), ConsoleColor.Green);
            }
            else
            {
                SetConsoleColor(string.Format("При добавлении произошла ошибка", newMedOrganization.NameOfOrganization), ConsoleColor.Red);
            }
        }
        public static void PrintPacient()
        {
            foreach (Pacient item in ServicePacient.GetPacient())
            {
                Console.WriteLine("{0} ({1}), IIN - {2}", item.FIO, item.Sex, item.IIN);
            }
        }
        public static void AddPacient()
        {
            Pacient newPacient = new Pacient();
            Console.Write("Введите ФИО: ");
            newPacient.FIO = Console.ReadLine();
            Console.Write("Введите ИИН: ");
            newPacient.IIN = Console.ReadLine();
            Console.Write("Введите дату рождения: ");
            newPacient.DoB = DateTime.Parse(Console.ReadLine());
            Console.Write("Введите пол (0-ж,1=м): ");
            newPacient.Sex = (Gender)Int32.Parse(Console.ReadLine());
            if (ServicePacient.AddPacient(newPacient))
            {
                SetConsoleColor(string.Format("Пациент {0} добавлен", newPacient.FIO), ConsoleColor.Green);
            }
            else
            {
                SetConsoleColor(string.Format("При добавлении произошла ошибка"), ConsoleColor.Red);
            }
        }
    }
}
