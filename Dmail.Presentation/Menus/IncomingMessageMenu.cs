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
using Dmail.Presentation.Actions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
                var actions = new GettingMessages();
                Console.Clear();
                Console.WriteLine("Izaberite opciju");
                Console.WriteLine("1 - Pročitana pošta");
                Console.WriteLine("2 - Nepročitana pošta");
                Console.WriteLine("3 - Pošta određenoga pošiljatelja");
                Console.WriteLine("0 - Povratak na glavni menu");
                var choice = Console.ReadLine();
                int.TryParse(choice, out _choice);
                if (_choice == 0 && choice!="0")
                {
                    Console.WriteLine("Nije upisan valjani input");
                    Console.ReadLine();
                    continue;
                }
                switch (_choice)
                {
                    case 1:
                        actions.GetSeenMessages(Info.Repos.UserRepo, Info.Repos.MessageRepo, false);
                        break;
                    case 2:
                       actions.GetNonSeenMessages(Info.Repos.UserRepo, Info.Repos.MessageRepo, Info.Repos.MessageReceiversRepo, false);
                        break;
                    case 3:
                       actions.GetMessagesbySender(Info.Repos.UserRepo, Info.Repos.MessageRepo, false);
                        break;
                    case 0:
                        return;
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
        
        public static void MessagesMenu(ICollection<MessagePrint> messages, int seen)
        {
            while (true)
            {
                Console.Clear();
                messages=messages.OrderByDescending(x => x.CreatedAt).ToList();
                if (messages.Count == 0)
                {
                    Console.WriteLine("Nije pronađena ni jedna poruka");
                    Console.ReadLine();
                    return;
                }
                PrintMessages(messages);
                Console.WriteLine("Upišite što želite s porukama");
                Console.WriteLine("1 - Detaljni ispis");
                Console.WriteLine("2 - Ispiši samo mailove ");
                Console.WriteLine("3 - Ispiši samo događaje");
                Console.WriteLine("0 ili bilo koji drugi input - Nazad");
                var choice = Console.ReadLine();
                var messagesCopy = messages.Where(x => x.IsEvent == false).ToList();
                var eventsCopy = messages.Where(x => x.IsEvent == true).ToList();

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        messages = ChooseMessage(Info.Repos.UserRepo, Info.Repos.MessageRepo, Info.Repos.MessageReceiversRepo, messages, seen);
                        break;
                    case "2":
                        Console.Clear();
                        Console.WriteLine("Ispis svih mailova");
                        messages = ChooseMessage(Info.Repos.UserRepo, Info.Repos.MessageRepo, Info.Repos.MessageReceiversRepo, messagesCopy, seen);
                        foreach(var eventEnumerate in eventsCopy)
                        {
                            messages.Add(eventEnumerate);
                        }
                        break;
                    case "3":
                        Console.Clear();
                        Console.WriteLine("Ispis svih događaja");
                        messages = ChooseMessage(Info.Repos.UserRepo, Info.Repos.MessageRepo, Info.Repos.MessageReceiversRepo, eventsCopy, seen);
                        foreach (var messageEnumerate in messagesCopy)
                        {
                            messages.Add(messageEnumerate);
                        }
                        break;
                    default:
                        return;
                }
            }
        }
        public static ICollection<MessagePrint> ChooseMessage(UserRepo userRepo, MessageRepo messageRepo, MessageReceiversRepo messageReceiversRepo, ICollection<MessagePrint> messages, int seen)
        {
            var actions = new MessageActions();
            while (true)
            {
                PrintMessages(messages);
                Console.WriteLine("Detaljan ispis poruka");
                Console.WriteLine("Upišite redni broj koje poruke želite pristupiti");
                var select = Console.ReadLine();
                var selectId = -1;
                int.TryParse(select, out selectId);
                if (selectId == 0 && select=="0")
                    return messages;
                if (selectId == 0 && select != "0")
                {
                    Console.WriteLine("Upisan krivi unos");
                    Console.ReadLine();
                    continue;
                }
                if (selectId < 0 || selectId>messages.Count())
                {
                    Console.WriteLine("Nije upisan validan broj poruke");
                    Console.ReadLine();
                    continue;
                }
                var message = messages.ElementAt(selectId - 1);
                messages = DetailedMessageMenu(message, messageRepo, messageReceiversRepo, userRepo, messages,seen);

            }
        }
        public static ICollection<MessagePrint> DetailedMessageMenu(MessagePrint message, MessageRepo messageRepo, MessageReceiversRepo messageReceiversRepo, UserRepo userRepo, ICollection<MessagePrint> messages, int seen)
        {
            var selectId = -1;
            var actions = new MessageActions();
            actions.GetMessageAndUpdateSeenStatus(message, messageReceiversRepo);
            if (seen==0)
                messages.Remove(message);   
            var userActions = new AccounActions();
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
                    return messages;
                switch (selectId.ToString())
                {
                    case "1":
                        var confirmation = MainMenu.ConfirmationDialog();
                        if (!confirmation)
                            break;
                        var checkResponse = actions.SetMessageAsUnseen(message, messageReceiversRepo, Info.UserId);
                        if (checkResponse != ResponseType.Success)
                        {
                            Console.WriteLine("Došlo je do greške");
                            Console.ReadLine();
                            break;
                        }
                        Console.WriteLine("Uspješno poruka označena kao nepročitana");
                        if (seen==0)
                            messages.Add(message);
                        else if (seen==1)
                            messages.Remove(message);
                        Console.ReadLine();
                        return messages;
                    case "2":
                        var confirmation1 = MainMenu.ConfirmationDialog();
                        if (!confirmation1)
                            break;
                        var check = userActions.CreateNewSpamConnection(message.SenderId);
                        if (check != ResponseType.Success)
                        {
                            Console.WriteLine("Došlo je do greške pri stvaranju spem konekcije ili već postoji ta spam konekcija");
                            Console.ReadLine();
                            break;
                        }
                        messages.Remove(message);
                        Console.WriteLine("Uspješno dodana spam konekcija između korisnika " + userActions.GetUserEmail(Info.UserId) + " i " + message.SenderEmail);
                        Console.ReadLine();
                        return messages;
                    case "3":
                        var confirmation2 = MainMenu.ConfirmationDialog();
                        if (!confirmation2)
                            break;
                        var check2 = actions.DeleteMessageConnection(message.Id, Info.UserId, messageReceiversRepo);
                        if (message.AllEmails.Count() == 1)
                            actions.DeleteMessage(messageRepo,message.Id);
                        messages.Remove(message);
                        if (check2 != ResponseType.Success)
                        {
                            Console.WriteLine("Došlo je do pogreške pri brisanju maila");
                            Console.ReadLine();
                            break;
                        }
                        Console.WriteLine("Uspješno izbrisan mail");
                        Console.ReadLine();
                        return messages;    
                    case "4":
                        if (message.IsEvent)
                        {
                            Console.WriteLine("Upišite 0 da biste odbili događaj i 1 da biste ga prihvatili");
                            var confirm = Console.ReadLine();
                            var confirmation3 = MainMenu.ConfirmationDialog();
                            var answer = "Odbijen";
                            if (!confirmation3)
                                return messages;
                            if (confirm == "1")
                                answer = "Prihvaćen";
                            var newMessage = actions.GenerateMessage(messageRepo, "Odgovor na "+message.Title, answer);
                            var check3 = actions.AnswerEvent(messageReceiversRepo, Info.UserId, message.Id,  confirm == "1");
                            if (check3 != ResponseType.Success)
                            {
                                Console.WriteLine("Već ste odgovorili na ovaj event istim odgovorom, ako želite poslati novu poruku na event onda morate imati izmijenjen odgovor");
                                Console.ReadLine();
                                break;
                            }
                            var check4 = actions.MakeConnection(messageReceiversRepo, newMessage, message.SenderId);
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
                        NewMessageMenu.NewMessageContent(new List<int>() { message.SenderId }, userRepo, messageRepo, messageReceiversRepo);
                        break;
                    default:
                        Console.WriteLine("Nije upisan pravi input");
                        Console.ReadLine(); break;
                }
            }
            return messages;

        }
    }
}