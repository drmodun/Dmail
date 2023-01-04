using Dmail.Domain.Enums;
using Dmail.Domain.Repositories;
using Dmail.Domain.Factories;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace Dmail.Presentation.Menus
{
    public static class MainMenu
    {
        public static int _choice;
        public static UserRepo userRepo;
        public static void Content()
        {
            while (true)
            {
                _choice = -1;
                Console.Clear();
                Console.WriteLine("Upišite željenu opciju");
                Console.WriteLine("1 - Ulazna pošta");
                Console.WriteLine("2 - Izlazna pošta");
                Console.WriteLine("3 - Spam");
                Console.WriteLine("4 - Pošalji novi mail");
                Console.WriteLine("5 - Pošalji novi događaj");
                Console.WriteLine("6 - Postavke profila");
                Console.WriteLine("7 - Odjava iz profila");
                int.TryParse(Console.ReadLine(), out _choice);
                if (_choice < 0 || _choice > 7)
                {
                    Console.WriteLine("Upisan netočan input");
                    Console.ReadLine();
                    continue;
                }
                switch (_choice)
                {
                    case 0:
                        Environment.Exit(0);
                        break;
                    case 1:
                        IncomingMessageMenu.Content();
                        break;
                    case 2:
                        OutgoingMessageMenu.GetSentMessages(userRepo, new MessageRepo(DmailDbContextFactory.GetDmailContext()));
                        break;
                    case 3:
                        SpamMenu.Content();
                        break;
                    case 4:
                        NewMessageMenu.Content();
                        break;
                    case 7:
                        return;

                }

            }
        }
        public static bool ConfirmationDialog()
        {
            int confrimation = 0;
            Console.WriteLine("Ova akcija će trajno promijeniti podatke aplikacije, jeste li sigurni da ju želite napraviti");
            Console.WriteLine("1 - Da");
            Console.WriteLine("0 - Ne");
            int.TryParse(Console.ReadLine(), out confrimation);
            if (confrimation == 1)
                return true;
            return false;
        }
        public static void Accounts()
        {
            while (true)
            {
                _choice = -1;
                Console.Clear();
                Console.WriteLine("Računi");
                Console.WriteLine("Odaberite");
                Console.WriteLine("1 - Postojeći račun");
                Console.WriteLine("2 - Novi račun");
                Console.WriteLine("0 - Izlaz iz aplikacije");
                int.TryParse(Console.ReadLine(), out _choice);
                switch (_choice)
                {
                    case 0:
                        Environment.Exit(0);
                        break;
                    case 1:
                        AccountMenus.AuthMenu(userRepo);
                        break;
                    case 2:
                        AccountMenus.NewAccount(userRepo);
                        break;
                    default:
                        Console.WriteLine("Krivi upis");
                        Console.ReadLine();
                        break;
                }


            }
        }
    }
}