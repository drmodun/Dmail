using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dmail.Domain.Repositories;
using Dmail.Presentation;
using Dmail.Domain.Factories;
using System.Security.Cryptography.X509Certificates;

namespace Dmail.Presentation.Menus
{
    public class IncomingMessageMenu
    {
        private static int _choice;

        public static void Content()
        {
            _choice = -1;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Izaberite opciju");
                Console.WriteLine("1 - Pročitana pošta");
                Console.WriteLine("2 - Nepročitana pošta");
                Console.WriteLine("3 - Pošta određenoga pošiljatelja");
                Console.WriteLine("0 - Povratak na glavni menu");
                int.TryParse(Console.ReadLine(), out _choice);
                if (_choice < 0)
                {
                    Console.WriteLine("Nije upisan valjani input");
                    Console.ReadLine();
                }
                else if (_choice == 0)
                    return;
                switch (_choice)
                {
                    case 1:
                        GetSeenMessages(MainMenu.userRepo, new MessageRepo(DmailDbContextFactory.GetDmailContext()));
                        break;
                    case 2:
                        GetNonSeenMessages(MainMenu.userRepo, new MessageRepo(DmailDbContextFactory.GetDmailContext()), new MessageReceiversRepo(DmailDbContextFactory.GetDmailContext()));
                        break;
                    case 3:
                        GetMessagesbySender(MainMenu.userRepo, new MessageRepo(DmailDbContextFactory.GetDmailContext()), new MessageReceiversRepo(DmailDbContextFactory.GetDmailContext()));
                        break;
                }
            }

        }
        public static void GetSeenMessages(UserRepo userRepo, MessageRepo messageRepo)
        {
            Console.WriteLine("Pročitane poruke");
            Console.WriteLine("Upišite broj poruke ili događaja za više akcija");
            var messages = messageRepo.GetSeenMessages(AccountMenus.UserId);
            var iterator = 1;
            foreach (var message in messages)
            {
                Console.WriteLine(iterator.ToString());
                Prints.PrintMessage(message);
                Console.WriteLine(" ");
                iterator++;
            }
            Console.ReadLine();
        }
        public static void GetNonSeenMessages(UserRepo userRepo, MessageRepo messageRepo, MessageReceiversRepo messageReceiversRepo)
        {
            Console.WriteLine("Nepročitane poruke");
            Console.WriteLine("Upišite broj poruke ili događaja za više akcija");
            var messages = messageRepo.GetNonSeenMessages(AccountMenus.UserId);
            var iterator = 1;
            foreach (var message in messages)
            {
                Console.WriteLine(iterator.ToString());
                Prints.PrintMessage(message);
                Console.WriteLine(" ");
                messageReceiversRepo.Update(message.Id, AccountMenus.UserId, true);
                iterator++;
            }
            Console.ReadLine();
        }
        public static void GetMessagesbySender(UserRepo userRepo, MessageRepo messageRepo, MessageReceiversRepo messageReceiversRepo)
        {
            var senderId = -1;
            Console.WriteLine("Poruke od specifičnog pošiljatelja");
            while (true)
            {
                Console.WriteLine("Upišite od kojeg pošiljatelja želite vidjeti poruke (upišite 0 za povratak na prijašnji menu)");
                var sender = Console.ReadLine();
                senderId = userRepo.GetIdByEmail(sender);
                if (sender == "0")
                    return;
                if (senderId == -1)
                {
                    Console.WriteLine("Taj korisnik nije pronađen");
                    Console.ReadLine();
                    continue;
                }
                break;
            }
            var messages = messageRepo.GetMessagesBySender(AccountMenus.UserId, senderId);
            var iterator = 1;
            foreach(var message in messages)
            {
                Console.WriteLine(iterator.ToString());
                Prints.PrintMessage(message);
                Console.WriteLine(" ");
                messageReceiversRepo.Update(message.Id, AccountMenus.UserId, true);
                iterator++;


            }
            Console.ReadLine();

        }
    }
}
//questions
/*how many seed data tests
 * are events and emails supposed to be two diffwereent tsbles
 * how much linq do we have to use
 * how much abstraction muzsta we do on presentation layer
 * can I use stratic classesd fgor menus and stuff
 * do we have to use interfaces and stuff like that*/