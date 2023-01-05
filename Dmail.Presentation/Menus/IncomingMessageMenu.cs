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
using Dmail.Data.Entities.Models;

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
                        GetSeenMessages(MainMenu.userRepo, new MessageRepo(DmailDbContextFactory.GetDmailContext()), false);
                        break;
                    case 2:
                        GetNonSeenMessages(MainMenu.userRepo, new MessageRepo(DmailDbContextFactory.GetDmailContext()), new MessageReceiversRepo(DmailDbContextFactory.GetDmailContext()), false);
                        break;
                    case 3:
                        GetMessagesbySender(MainMenu.userRepo, new MessageRepo(DmailDbContextFactory.GetDmailContext()), false);
                        break;
                    default:
                        Console.WriteLine("Nije upisan točni input");
                        Console.ReadLine();
                        break;
                }
            }

        }
        public static void PrintMessages(ICollection<MessagePrint> messages)
        {
            var iterator = 0;
            foreach (var message in messages)
            {
                iterator++;
                Console.WriteLine($"{iterator} {message.Title} {message.SenderEmail}");
                Console.WriteLine(" ");
            }
        }
        public static void GetSeenMessages(UserRepo userRepo, MessageRepo messageRepo, bool spam)
        {
            Console.WriteLine("Pročitane poruke");
            ICollection<MessagePrint> messages = null;
            if (!spam)
            {
                messages = messageRepo.GetSeenMessages(AccountMenus.UserId);
            }
            else
            {
                messages = SpamMenu.spamRepo.GetSeenMessages(AccountMenus.UserId);
            }
            Console.Clear();
            MessagesMenu(messages);

        }
        public static void GetNonSeenMessages(UserRepo userRepo, MessageRepo messageRepo, MessageReceiversRepo messageReceiversRepo, bool spam)
        {
            Console.Clear();
            Console.WriteLine("Nepročitane poruke");
            ICollection<MessagePrint> messages = null;
            if (!spam)
            {
                messages = messageRepo.GetNonSeenMessages(AccountMenus.UserId);
            }
            else
            {
                messages = SpamMenu.spamRepo.GetNonSeenMessages(AccountMenus.UserId);
            };
            Console.Clear();
            MessagesMenu(messages);

        }
        public static void GetMessagesbySender(UserRepo userRepo, MessageRepo messageRepo, bool spam)
        {
            var senderId = -1;
            Console.WriteLine("Poruke od specifičnog pošiljatelja");
            var sender = Console.ReadLine();
            ICollection<MessagePrint> messages = null;
            if (!spam)
            {
                messages = messageRepo.GetMessagesBySender(AccountMenus.UserId, sender);
            }
            else
            {
                messages = SpamMenu.spamRepo.GetMessagesBySender(AccountMenus.UserId, sender);
            }
            if (messages.Count() == 0)
            {
                Console.WriteLine("Nije pronađena ni jedna poruka poslana od tog maila");
                Console.ReadLine();
                return;
            }
            Console.Clear();
            MessagesMenu(messages);

        }
        public static void MessagesMenu(ICollection<MessagePrint> messages)
        {
            while (true)
            {
                PrintMessages(messages);
                Console.WriteLine("Upišite što želite s porukama");
                Console.WriteLine("1 - Detaljni ispis");
                Console.WriteLine("2 - Ispiši samo mailove ");
                Console.WriteLine("3 - Ispiši samo događaje");
                Console.WriteLine("0 ili bilo koji drugi input - Nazad");
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        ChooseMessage(MainMenu.userRepo, messageRepo, messageReceiversRepo, messages);
                        break;
                    case "2":
                        var messagesCopy = messages.Where(x => x.IsEvent == false).ToList();
                        Console.Clear();
                        Console.WriteLine("Ispis svih mailova");
                        PrintMessages(messagesCopy);
                        ChooseMessage(MainMenu.userRepo, messageRepo, messageReceiversRepo, messagesCopy);
                        break;
                    case "3":
                        var eventsCopy = messages.Where(x => x.IsEvent == true).ToList();
                        Console.Clear();
                        Console.WriteLine("Ispis svih događaja");
                        PrintMessages(eventsCopy);
                        ChooseMessage(MainMenu.userRepo, messageRepo, messageReceiversRepo, eventsCopy);
                        break;
                    default:
                        return;
                }
            }
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
                if (selectId == 0)
                    return;
                if (selectId < 0 || selectId>messages.Count())
                {
                    Console.WriteLine("Nije upisan validan broj poruke");
                    Console.ReadLine();
                    continue;
                }
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
                            messages.Remove(message);
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
                            messages.Remove(message);
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
                                var check3 = messageReceiversRepo.UpdateAnswerToEvent(message.Id, AccountMenus.UserId, confirm == "1");
                                if (check3 != ResponseType.Success)
                                {
                                    Console.WriteLine("Već ste odgovorili na ovaj event istim odgovorom, ako želite poslati novu poruku na event onda morate imati izmijenjen odgovor");
                                    Console.ReadLine();
                                    break;
                                }
                                var check4 = messageReceiversRepo.NewConnection(newMessage, message.SenderId);
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
                            NewMessageMenu.NewMessageContent(new List<int>() { message.SenderId}, userRepo, messageRepo, messageReceiversRepo);
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