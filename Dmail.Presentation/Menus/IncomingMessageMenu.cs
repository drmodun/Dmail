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
        
        public static void MessagesMenu(ICollection<MessagePrint> messages)
        {
            while (true)
            {
                Console.Clear();
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
                        ChooseMessage(Info.Repos.UserRepo, Info.Repos.MessageRepo, Info.Repos.MessageReceiversRepo, messages);
                        break;
                    case "2":
                        var messagesCopy = messages.Where(x => x.IsEvent == false).ToList();
                        Console.Clear();
                        Console.WriteLine("Ispis svih mailova");
                        PrintMessages(messagesCopy);
                        ChooseMessage(Info.Repos.UserRepo, Info.Repos.MessageRepo, Info.Repos.MessageReceiversRepo, messagesCopy);
                        break;
                    case "3":
                        var eventsCopy = messages.Where(x => x.IsEvent == true).ToList();
                        Console.Clear();
                        Console.WriteLine("Ispis svih događaja");
                        PrintMessages(eventsCopy);
                        ChooseMessage(Info.Repos.UserRepo, Info.Repos.MessageRepo, Info.Repos.MessageReceiversRepo, eventsCopy);
                        break;
                    default:
                        return;
                }
            }
        }
        public static void ChooseMessage(UserRepo userRepo, MessageRepo messageRepo, MessageReceiversRepo messageReceiversRepo, ICollection<MessagePrint> messages)
        {
            var actions = new MessageActions();
            while (true)
            {
                Console.WriteLine("Detaljan ispis poruka");
                Console.WriteLine("Upišite redni broj koje poruke želite pristupiti");
                var select = Console.ReadLine();
                var selectId = -1;
                int.TryParse(select, out selectId);
                if (selectId == 0 && select=="0")
                    return;
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
                actions.GetMessageAndUpdateSeenStatus(message, messageReceiversRepo);
                DetailedMessageMenu(message, messageRepo, messageReceiversRepo, userRepo, messages);

            }
        }
        public static bool DetailedMessageMenu(MessagePrint message, MessageRepo messageRepo, MessageReceiversRepo messageReceiversRepo, UserRepo userRepo, ICollection<MessagePrint> messages)
        {
            var selectId= -1;
            var actions = new MessageActions();
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
                    return false;
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
                        Console.ReadLine();
                        break;
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
                        break;
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
                        return true;
                    case "4":
                        if (message.IsEvent)
                        {
                            Console.WriteLine("Upišite 0 da biste odbili događaj i 1 da biste ga prihvatili");
                            var confirm = Console.ReadLine();
                            var confirmation3 = MainMenu.ConfirmationDialog();
                            var answer = "Odbijen";
                            if (!confirmation3)
                                return false;
                            if (confirm == "1")
                                answer = "Prihvaćen";
                            var newMessage = actions.GenerateAnswer(messageRepo, message, answer);
                            var check3 = actions.AnswerEvent(messageReceiversRepo, Info.UserId, message.Id,  confirm == "1");
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
                        NewMessageMenu.NewMessageContent(new List<int>() { message.SenderId }, userRepo, messageRepo, messageReceiversRepo);
                        break;
                    default:
                        Console.WriteLine("Nije upisan pravi input");
                        Console.ReadLine(); break;
                }
            }
            return false;

        }
    }
}