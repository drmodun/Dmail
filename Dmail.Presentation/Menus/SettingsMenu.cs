using Dmail.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Dmail.Presentation.Menus
{
    public static class SettingsMenu
    {
        public static void Content()
        {
            Console.Clear();
            PrintUsers();
        }
        public static void PrintUsers()
        {
            Console.Clear();
            Console.WriteLine("Korisnici koji šalju");
            var allSenders = IncomingMessageMenu.messageRepo.GetSenderUsers(AccountMenus.UserId);
            if (allSenders.Count() == 0)
            {
                Console.WriteLine("Dosada ovome mailu nije upućena ni jedna poruka");
                Console.ReadLine();
                return;
            }
            Prints.PrintUsers(allSenders);
            Console.WriteLine("Korisnici kojima ste slali:");
            var allReceivers = IncomingMessageMenu.messageRepo.GetReceiverUsers(AccountMenus.UserId);
            if (allReceivers.Count() == 0)
            {
                Console.WriteLine("Izgleda da nema maila poslanog na bilo kojeg korisnika trenutno");
                Console.ReadLine();
                return;
            }
            Prints.PrintUsers(allReceivers);
            Menu(allSenders, allReceivers);
        }
        public static void Menu(ICollection<UserPrint> allSenders, ICollection<UserPrint> allReceivers)
        {
            while (true)
            {
                Console.WriteLine("Upišite što želite s korisnicima napraviti");
                Console.WriteLine("1 - filter na korisnike koje ste blokirali");
                Console.WriteLine("2 - filter na korisnike koje niste blokirali");
                Console.WriteLine("3 - Opcije za blokiranje koriniska");
                var choice = Console.ReadLine();
                ICollection<UserPrint> sendersCopy;
                ICollection<UserPrint> receiversCopy; 
                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        sendersCopy = allSenders.Where(x => x.Blocked).ToList();
                        receiversCopy = allReceivers.Where(x => x.Blocked).ToList();
                        Console.WriteLine("Ispis samo blokiranih korisnika (pošiljatelji)");
                        Prints.PrintUsers(sendersCopy);
                        Console.WriteLine("Ispis samo blokiranih primatelja");
                        Prints.PrintUsers(receiversCopy);
                        ChooseUser(sendersCopy);
                        break;
                    case "2":
                        Console.Clear();
                        sendersCopy = allSenders.Where(x => x.Blocked==false).ToList();
                        receiversCopy = allReceivers.Where(x => x.Blocked==false).ToList();
                        Console.WriteLine("Ispis samo neblokiranih korisnika (pošiljatelji)");
                        Prints.PrintUsers(sendersCopy);
                        Console.WriteLine("Ispis samo neblokiranih primatelja");
                        Prints.PrintUsers(receiversCopy);
                        ChooseUser(sendersCopy);
                        break;
                    case "3":
                        ChooseUser(allSenders);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Krivi upis");
                        Console.ReadLine();
                        break;

                }
            }
        }
        public static void ChooseUser(ICollection<UserPrint> allSenders)
        {
            while (true)
            {
                Console.WriteLine("Izaberite korisnika kojemu želite promjeniti status blokiranja");
                Console.WriteLine("Možete blokirati ili odblokirati samo korisnike koji su vam slali poruke");
                var choiceTry = Console.ReadLine();
                var choice = -1;
                int.TryParse(choiceTry, out choice);
                if (choice < 0 || choice > allSenders.Count())
                {
                    Console.WriteLine("NIje upisan validan broj korisnika");
                    Console.ReadLine();
                    continue;
                }
                else if (choice == 0)
                {
                    return;
                }
                else
                {
                    SpamMenu.Content(allSenders.ElementAt(choice - 1));
                    return;
                }
            }
        }
    }
}
