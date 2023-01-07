using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Dmail.Domain.Repositories;
using Dmail.Domain.Enums;
using Dmail.Domain.Factories;
using System.Reflection;
using Dmail.Presentation.Actions;

namespace Dmail.Presentation.Menus
{
    public static class NewMessageMenu
    {
        public static int _choice;
        public static void Content()
        {
            while (true)
            {
                _choice = -1;
                Console.Clear();
                Console.WriteLine("Nova pošta");
                Console.WriteLine("Izaberite opciju");
                Console.WriteLine("1 - Nova pošta");
                Console.WriteLine("0 - Main Menu");
                var choice = Console.ReadLine();  
                int.TryParse(choice, out _choice);
                if (_choice == 0 && choice != "0" )
                {
                    Console.WriteLine("Nije upisan valjani broj");
                    Console.ReadLine();
                    continue;
                }
                switch (_choice)
                {
                    case 1:
                        NewMessage(Info.Repos.UserRepo, Info.Repos.MessageRepo, Info.Repos.MessageReceiversRepo);
                        break;
                    case 2:
                        return;
                    default: Console.WriteLine("Krivi input");
                        Console.ReadLine();
                        break;
                }
                //new message system
            }
        }
        public static void NewMessage(UserRepo userRepo, MessageRepo messageRepo, MessageReceiversRepo messageReceiversRepo)
        {
            var actions = new AccounActions();
            while (true)
            {
                Console.WriteLine("Početak nove poruke");
                Console.Clear();
                Console.WriteLine("Nova poruka");
                Console.WriteLine("Upišite ovdje podatke nove poruke, za povrataka upišite 0 u bilo koje polje");
                Console.WriteLine("Upišite mailove ljudi kojima šaljete mail");
                var emails = Console.ReadLine().Split(",");
                var emailIds = new List<int>();
                if (emails.Contains("0"))
                    return;
                var emailCheck = true;
                if (emails.Length == 0)
                {
                    Console.WriteLine("Niste upisali ni jedan mail");
                    Console.ReadLine();
                    continue;
                }
                foreach (var email in emails)
                {
                    var id = actions.GetUserIdByEmail(email, userRepo);
                    if (id == -1)
                    {
                        Console.WriteLine("Neki od mailova ne postoje");
                        Console.ReadLine();
                        emailCheck = false;
                        continue;
                    }
                    if (emailIds.Contains(id))
                    {
                        Console.WriteLine("Ne možete istoj osobi dva puta poslati isti mail");
                        Console.ReadLine();
                        emailCheck = false;
                        continue;
                    }
                    emailIds.Add(id);
                }
                if (!emailCheck)
                    continue;
                var check = NewMessageContent(emailIds, userRepo, messageRepo, messageReceiversRepo);
                if (check)
                    return;
            }
        }
        public static bool NewMessageContent(List<int> emailIds, UserRepo userRepo, MessageRepo messageRepo, MessageReceiversRepo messageReceiversRepo)
        {
            var actionsMessages = new MessageActions();
            var actions = new AccounActions();
            Console.WriteLine("Upišite naslov maila:");
            var title = Console.ReadLine();
            if (title == "0")
                return false;
            if (title.Trim().Length == 0)
            {
                Console.WriteLine("Netočan naslov maila");
                Console.ReadLine();
                return false;
            }
            Console.WriteLine("Upišite tijelo maila");
            var body = Console.ReadLine();
            if (body == "0")
                return false;
            var check1 = 0;
            var confirmation = MainMenu.ConfirmationDialog();
            if (!confirmation)
            {
                return false;
            }
            check1 = actionsMessages.GenerateMessage(messageRepo, title, body);
            if (check1 == -1)
            {
                Console.WriteLine("Došlo je do problema pri pravljenju poruke");
                Console.ReadLine();
                return false;
            }
            foreach (var item in emailIds)
            {
                
                var check2 = actionsMessages.MakeConnection(messageReceiversRepo, check1, item);
                if (check1 == -1 || check2 != ResponseType.Success) 
                {
                    Console.WriteLine(check2.ToString());
                    Console.WriteLine(check1.ToString());
                    Console.WriteLine($"Došlo je do greške pri pravljenju maila osobi {actions.GetUserEmail(item)}");
                    Console.ReadLine();
                    continue;
                }

            }
            if (check1 != -1)
            {
                Console.WriteLine("Uspješno napravljenja poruka i poslan mail");
                Console.ReadLine();
                return true;
            }

            return true;

        }

    
    public static void NewEvent(UserRepo userRepo, MessageRepo messageRepo, MessageReceiversRepo messageReceiversRepo)
    {
            var actions = new AccounActions();
            var actionMessages = new MessageActions();
        while (true)
        {
            Console.WriteLine("Početak novog događaja");
            Console.Clear();
            Console.WriteLine("Nova poruka");
            Console.WriteLine("Upišite ovdje podatke nove poruke, za povrataka upišite 0 u bilo koje polje");
            Console.WriteLine("Upišite mailove ljudi kojima šaljete događaj");
            var emails = Console.ReadLine().Split(",");
            var emailIds = new List<int>();
            if (emails.Contains("0"))
                return;
            foreach (var email in emails)
            {
                var id = actions.GetUserIdByEmail(email, userRepo);
                if (id == -1)
                {
                    Console.WriteLine("Neki od mailova ne postoje");
                    Console.ReadLine();
                    continue;
                }
                if (emailIds.Contains(id) || emailIds.Contains(Info.UserId))
                {
                    Console.WriteLine("Ne možete istoj osobi dva puta poslati isti događaj ili samome sebi poslati isti događaj");
                    Console.ReadLine();
                    continue;
                }
                emailIds.Add(id);
            }
            Console.WriteLine("Upišite naslov događaja:");
            var title = Console.ReadLine();
            if (title == "0")
                return;
            if (title.Trim().Length == 0)
            {
                Console.WriteLine("Netočan naslov događaja");
                Console.ReadLine();
                continue;
            }
            Console.WriteLine("Upišite vrijeme događaja");
            var dateTry = Console.ReadLine();
            string[] formats = { "dd/MM/yyyy", "dd/M/yyyy", "d/M/yyyy", "d/MM/yyyy",
                    "dd/MM/yy", "dd/M/yy", "d/M/yy", "d/MM/yy", "dd/MM/yyyy HH:mm:ss","dd/MM/yyyy HH:mm", "dd/M/yyyy HH:mm", "d/M/yyyy HH:mm", "d/MM/yyyy HH:mm",
                    "dd/MM/yy HH:mm", "dd/M/yy HH:mm", "d/M/yy HH:mm", "d/MM/yy HH:mm", "yyyy/MM/dd H:mm"};
            var date = DateTime.MinValue;
            DateTime.TryParseExact(dateTry, formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out date);
            if (date < DateTime.Now)
            {
                Console.WriteLine("Nije upisan validan datum");
                Console.ReadLine();
                continue;
            }
            var dateConverted = date.ToUniversalTime();
            var check1 = 0;
            var confirmation = MainMenu.ConfirmationDialog();
            if (!confirmation)
            {
                return;
            }
            check1 = actionMessages.GenerateEvent(messageRepo, title, dateConverted);
            if (check1 == -1)
            {
                Console.WriteLine("Došlo je do problema pri pravljenju poruke");
                Console.ReadLine();
                break;
            }
            foreach (var item in emailIds)
            {

                var check2 = actionMessages.MakeConnection(messageReceiversRepo,check1, item);
                if (check1 == -1 || check2 != ResponseType.Success)
                {
                    Console.WriteLine(check2.ToString());
                    Console.WriteLine(check1.ToString());
                    Console.WriteLine($"Došlo je do greške pri pravljenju maila osobi {actions.GetUserEmail(item)}");
                    Console.ReadLine();
                    continue;
                }

            }
            if (check1 != -1)
            {
                Console.WriteLine("Uspješno napravljenja poruka i poslan mail");
                Console.ReadLine();
                return;
            }

            return;

        }
    }
}
}
