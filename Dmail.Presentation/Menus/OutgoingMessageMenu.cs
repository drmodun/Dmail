﻿using Dmail.Data.Entities.Models;
using Dmail.Domain.Enums;
using Dmail.Domain.Models;
using Dmail.Domain.Repositories;
using Dmail.Presentation.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Dmail.Presentation.Menus
{
    public static class OutgoingMessageMenu
    {
        public static void PrintMessages(ICollection<MessagePrint> messages)
        {
            var iterator = 0;
            foreach (var message in messages) {
                iterator++;
                var emails = Prints.TranslatedEmails(message.AllEmails);
                Console.WriteLine($"{iterator} {message.Title} {string.Join(", ", emails)}");
                Console.WriteLine(" ");
                    }
        }
        public static void GetSentMessages(MessageRepo messageRepo)
        {
            var actions = new GettingMessages();
            Console.WriteLine("Poslane poruke");
            Console.WriteLine("Upišite broj poruke koje želite urediti");
            var messages = actions.GetSentMessages(messageRepo);
            Console.Clear();
            MessagesMenu(messages);
        }
        public static void ChooseMessage(MessageRepo messageRepo, MessageReceiversRepo messageReceiversRepo, ICollection<MessagePrint> messages)
        {
            while (true)
            {
                Console.WriteLine("Detaljan ispis poruka");
                var select = Console.ReadLine();
                var selectId = -1;
                int.TryParse(select, out selectId);
                if (selectId == 0) {
                    return;
                }
                if (selectId<0 || selectId>messages.Count())
                {
                    Console.WriteLine("Nije upisan validan broj poruke");
                    Console.ReadLine();
                    continue;
                }
                var message = messages.ElementAt(selectId - 1);
                Console.WriteLine(Info.UserId.ToString()+" "+message.Id.ToString());
                if (message.IsEvent)
                    Prints.PrintDetailedEvent(message, true);
                else
                    Prints.PrintDetailedMessage(message, true);
                messages = MessageDetailedMenu(message, selectId, messageRepo, messageReceiversRepo, messages);
                
            }
        }
        public static ICollection<MessagePrint> MessageDetailedMenu(MessagePrint message, int selectId, MessageRepo messageRepo, MessageReceiversRepo messageReceiversRepo, ICollection<MessagePrint> messages)
        {
            var actions = new MessageActions();
            while (true)
            {
                Console.WriteLine("Akcije");
                Console.WriteLine("1 - Izbriši poruku");
                Console.WriteLine("0 - Povratak na prijašnji menu");
                var choice = Console.ReadLine();
                int.TryParse(choice, out selectId);
                if (choice == "0")
                    return messages;
                switch (selectId.ToString())
                {
                    case "1":
                        var confirmation = MainMenu.ConfirmationDialog();
                        if (!confirmation)
                            return messages;
                        var check = ResponseType.Success;
                        foreach (var item in message.AllEmails)
                        {
                            check = actions.DeleteMessageConnection(message.Id, item, messageReceiversRepo);
                        }
                        check = actions.DeleteMessage(messageRepo, message.Id);
                        messages.Remove(message);
                        if (check != ResponseType.Success)
                        {
                            Console.WriteLine("Nije moguće izbrisati poruku");
                            Console.ReadLine();
                            return messages;
                        }

                        return messages;
                    default:
                        Console.WriteLine("Krivi upis");
                        Console.ReadLine();
                        break;
                }
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
                        ChooseMessage(Info.Repos.MessageRepo, Info.Repos.MessageReceiversRepo, messages);
                        break;
                    case "2":
                        var messagesCopy = messages.Where(x => x.IsEvent == false).ToList();
                        Console.Clear();
                        Console.WriteLine("Ispis svih mailova");
                        PrintMessages(messagesCopy);
                        ChooseMessage(Info.Repos.MessageRepo, Info.Repos.MessageReceiversRepo, messagesCopy);
                        break;
                    case "3":
                        var eventsCopy = messages.Where(x => x.IsEvent == true).ToList();
                        Console.Clear();
                        Console.WriteLine("Ispis svih događaja");
                        PrintMessages(eventsCopy);
                        ChooseMessage(Info.Repos.MessageRepo, Info.Repos.MessageReceiversRepo, eventsCopy);
                        break;
                    default:
                        return;
                }
            }
        }

    }
}