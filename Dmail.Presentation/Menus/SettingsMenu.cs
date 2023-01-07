using Dmail.Domain.Models;
using Dmail.Domain.Repositories;
using Dmail.Presentation.Actions;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Net.WebSockets;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Dmail.Presentation.Menus
{
    public static class SettingsMenu
    {
        
        public static void GetUsers(MessageRepo messageRepo)
        {
            Console.Clear();
            var actions = new GetUsers();
            Console.Clear();
            var allSenders = actions.GetSenderUsers(Info.UserId, messageRepo);
            if (allSenders.Count() == 0)
            {
                Console.WriteLine("Dosada ovome mailu nije upućena ni jedna poruka");
                Console.ReadLine();
                return;
            }
            var allReceivers =actions.GetReceiverUsers(Info.UserId, messageRepo);
            if (allReceivers.Count() == 0)
            {
                Console.WriteLine("Izgleda da nema maila poslanog na bilo kojeg korisnika trenutno");
                Console.ReadLine();
                return;
            }
            Menu(allSenders, allReceivers);
        }
        public static void Menu(ICollection<UserPrint> allSenders, ICollection<UserPrint> allReceivers)
        {

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Svi korisnici kojima ste slali");
                Prints.PrintUsers(allSenders);
                Console.WriteLine("Svi korisnici kojima saljete");
                Prints.PrintUsers(allReceivers);
                Console.WriteLine("Upišite što želite s korisnicima napraviti");
                Console.WriteLine("1 - filter na korisnike koje ste blokirali");
                Console.WriteLine("2 - filter na korisnike koje niste blokirali");
                Console.WriteLine("3 - Opcije za blokiranje koriniska");
                Console.WriteLine("0 - Main menu");
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
