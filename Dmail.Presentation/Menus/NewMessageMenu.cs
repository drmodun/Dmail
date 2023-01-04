using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Dmail.Domain.Repositories;
using Dmail.Domain.Enums;
using Dmail.Domain.Factories;
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
                int.TryParse(Console.ReadLine(), out _choice);
                if (_choice<0 || _choice > 1)
                {
                    Console.WriteLine("Nije upisan valjani broj");
                    Console.ReadLine();
                }
                if (_choice == 0)
                    return;
                switch(_choice)
                {
                    case 1:
                        NewMessage(MainMenu.userRepo, new MessageRepo(DmailDbContextFactory.GetDmailContext()), new MessageReceiversRepo(DmailDbContextFactory.GetDmailContext()));
                        break;
                }
                //new message system
            }
        }
        public static void NewMessage(UserRepo userRepo, MessageRepo messageRepo, MessageReceiversRepo messageReceiversRepo)
        {
            while (true)
            {
                Console.WriteLine("Početak nove poruke");
                Console.Clear();
                Console.WriteLine("Nova poruka");
                Console.WriteLine("Upišite ovdje podatke nove poruke, za povrataka upišite 0 u bilo koje polje");
                Console.WriteLine("Upišite mailove ljudi kojima šaljete mail");
                var emails = Console.ReadLine().Split(",");
                var emailIds= new List<int>();
                if (emails.Contains("0"))
                    return;
                foreach (var email in emails) {
                    var id = userRepo.GetIdByEmail(email);
                    if (id == -1)
                    {
                        Console.WriteLine("Neki od mailova ne postoje");
                        Console.ReadLine();
                        continue;
                    }
                    if (emailIds.Contains(id))
                    {
                        Console.WriteLine("Ne možete istoj osobi dva puta poslati isti mail");
                        Console.ReadLine();
                        continue;
                    }
                    emailIds.Add(id);
                }
                Console.WriteLine("Upišite naslov maila:");
                var title = Console.ReadLine();
                if (title=="0")
                    return;
                if (title.Trim().Length == 0)
                {
                    Console.WriteLine("Netočan naslov maila");
                    Console.ReadLine();
                    continue;
                }
                Console.WriteLine("Upišite tijelo maila");
                var body = Console.ReadLine();
                if (body=="0")
                    return;
                var check1 = 0;
                var confirmation = MainMenu.ConfirmationDialog();
                if (!confirmation)
                {
                    return;
                }
                foreach (var item in emailIds)
                {
                    check1 = messageRepo.NewMessage(AccountMenus.UserId, item, title, body);
                    if (check1 ==-1)
                    {
                        Console.WriteLine("Došlo je do problema pri pravljenju poruke");
                        Console.WriteLine(check1.ToString());
                        Console.ReadLine();
                        break;
                    }
                    var check2 = messageReceiversRepo.NewConnection(check1, item);
                    if (check1 == -1 || check2!=ResponseType.Success)
                    {
                        Console.WriteLine(check2.ToString());
                        Console.WriteLine(check1.ToString());
                        Console.WriteLine($"Došlo je do greške pri pravljenju maila osobi {userRepo.GetUser(item).Email}");
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
