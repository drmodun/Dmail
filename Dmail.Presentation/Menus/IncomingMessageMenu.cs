using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dmail.Domain.Repositories;
using Dmail.Presentation;
using Dmail.Domain.Factories;
using System.Security.Cryptography.X509Certificates;
using System.Reflection.Metadata.Ecma335;
using Dmail.Domain.Models;
using Dmail.Domain.Enums;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Dmail.Presentation.Menus
{
    public class IncomingMessageMenu
    {
        private static int _choice;
        public static MessageRepo messageRepo;
        public static MessageReceiversRepo messageReceiversRepo;

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
            ChooseMessage(userRepo, messageRepo, messageReceiversRepo, messages);

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
                iterator++;
            }
            Console.ReadLine();
            ChooseMessage(userRepo, messageRepo, messageReceiversRepo, messages);

        }
        public static void GetMessagesbySender(UserRepo userRepo, MessageRepo messageRepo, MessageReceiversRepo messageReceiversRepo)
        {
            var senderId = -1;
            Console.WriteLine("Poruke od specifičnog pošiljatelja");
            var sender = Console.ReadLine();
            var messages = messageRepo.GetMessagesBySender(AccountMenus.UserId, sender);
            if (messages.Count() == 0)
            {
                Console.WriteLine("Nije pronađena ni jedna poruka poslana od tog maila");
                Console.ReadLine();
                return;
            }
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
            ChooseMessage(userRepo, messageRepo, messageReceiversRepo, messages);

        }
        public static void ChooseMessage(UserRepo userRepo, MessageRepo messageRepo, MessageReceiversRepo messageReceiversRepo, ICollection<MessagePrint> messages)
        {
            while (true)
            {
                Console.WriteLine("Detaljan ispis poruka");
                Console.WriteLine("Upišite redni broj koje poruke želite pristupiti");
                var select = Console.ReadLine();
                var selectId = -1;
                int.TryParse(select, out selectId);
                if (selectId <= 0)
                {
                    Console.WriteLine("Nije upisan validan broj poruke");
                    Console.ReadLine();
                    continue;
                }
                Console.WriteLine("HELP");
                var message = messages.ElementAt(selectId-1);
                messageReceiversRepo.Update(message.Id, AccountMenus.UserId, true);
                if (message.IsEvent)
                    Prints.PrintDetailedEvent(message, messageReceiversRepo.GetStatus(AccountMenus.UserId, message.Id));
                else
                    Prints.PrintDetailedMessage(message);
                while (true)
                {
                    Console.WriteLine("Akcije");
                    Console.WriteLine("1 - Označi kao nepročitano");
                    Console.WriteLine("2 - Označi kao spam");
                    Console.WriteLine("3 - Izbriši mail");
                    if (message.IsEvent)
                        Console.WriteLine("4 - Odgovori na poziv za događaj");
                    else
                        Console.WriteLine("4 - Odgovori na mail");
                    Console.WriteLine("Upiši 0 za povratak na prijašnji menu");
                    var choice = Console.ReadLine();
                    int.TryParse(choice, out selectId);
                    if (choice == "0")
                        return;
                    switch (selectId.ToString())
                    {
                        case "1":
                            var confirmation = MainMenu.ConfirmationDialog();
                            if (!confirmation)
                                break;
                            messageReceiversRepo.Update(message.Id, AccountMenus.UserId, false);
                            Console.WriteLine("Uspješno poruka označena kao nepročitana");
                            Console.ReadLine();
                            break;
                        case "2":
                            var confirmation1 = MainMenu.ConfirmationDialog();
                            if (!confirmation1)
                                break;
                            var check = SpamMenu.spamRepo.TryAdd(AccountMenus.UserId, message.SenderId);
                            if (check != ResponseType.Success)
                            {
                                Console.WriteLine("Došlo je do greške pri stvaranju spem konekcije ili već postoji ta spam konekcija");
                                Console.ReadLine();
                                break;
                            }
                            Console.WriteLine("Uspješno dodana spam konekcija između korisnika " + userRepo.GetUser(AccountMenus.UserId).Email + " i " + message.SenderEmail);
                            Console.ReadLine();
                            break;
                        case "3":
                            var confirmation2 = MainMenu.ConfirmationDialog();
                            if (!confirmation2)
                                break;
                            var check2 = messageReceiversRepo.Delete(AccountMenus.UserId, message.Id);
                            if (message.AllEmails.Count() == 1)
                                messageRepo.Delete(message.Id);
                            if (check2 != ResponseType.Success)
                            {
                                Console.WriteLine("Došlo je do pogreške pri brisanju maila");
                                Console.ReadLine();
                                break;
                            }
                            Console.WriteLine("Uspješno izbrisan mail");
                            Console.ReadLine();
                            return;
                        case "4":
                            if (message.IsEvent)
                            {
                                Console.WriteLine("Upišite 0 da biste odbili događaj i 1 da biste ga prihvatili");
                                var confirm = Console.ReadLine();
                                var confirmation3 = MainMenu.ConfirmationDialog();
                                var answer = "Odbijen";
                                if (!confirmation3)
                                    return;
                                if (confirm == "1")
                                    answer = "Prihvaćen";
                                var newMessage = messageRepo.NewMessage(AccountMenus.UserId, false, DateTime.UtcNow, $"Odgovor na {message.Title}", answer);
                                var check3 = messageReceiversRepo.Update(message.Id, AccountMenus.UserId, confirm == "1");
                                if (check3 != ResponseType.Success)
                                {
                                    Console.WriteLine("Već ste odgovorili na ovaj event istim odgovorom, ako želite poslati novu poruku na event onda morate imati izmijenjen odgovor");
                                    Console.ReadLine();
                                    break;
                                }
                                var check4 = messageReceiversRepo.NewConnection(AccountMenus.UserId, newMessage);
                                if (check4 != ResponseType.Success)
                                {
                                    Console.WriteLine("Došlo je do pogrešku pri odgovaranju na poruku");
                                    Console.ReadLine();
                                    break;
                                }
                                Console.WriteLine("Uspješno napravljena poruka");
                                Console.ReadLine();
                                break;


                            }
                            NewMessageMenu.NewMessage(userRepo, messageRepo, messageReceiversRepo);
                            break;
                        default:
                            Console.WriteLine("Nije upisan pravi input");
                            Console.ReadLine() ; break;
                    }

                }
            }
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