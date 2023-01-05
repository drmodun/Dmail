using Dmail.Domain.Enums;
using Dmail.Domain.Models;
using Dmail.Domain.Repositories;
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
        public static void GetSentMessages(UserRepo userRepo, MessageRepo messageRepo)
        {
            Console.WriteLine("Poslane poruke");
            Console.WriteLine("Upišite broj poruke koje želite urediti");
            var iterator = 1;
            var messages = messageRepo.GetSentMessages(AccountMenus.UserId);
            foreach (var message in messages)
            {
                Console.WriteLine(iterator.ToString());
                Prints.PrintAsSender(message);
                Console.WriteLine(" ");
                iterator++;
            }
            Console.ReadLine();
            ChooseMessage(userRepo, messageRepo, IncomingMessageMenu.messageReceiversRepo, messages);
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
                if (selectId == 0) {
                    return;
                }
                if (selectId<0)
                {
                    Console.WriteLine("Nije upisan validan broj poruke");
                    Console.ReadLine();
                    continue;
                }
                var message = messages.ElementAt(selectId - 1);
                Console.WriteLine(AccountMenus.UserId.ToString()+" "+message.Id.ToString());
                if (message.IsEvent)
                    Prints.PrintDetailedEvent(message, true);
                else
                    Prints.PrintDetailedMessage(message);
                while (true)
                {
                    Console.WriteLine("Akcije");
                    Console.WriteLine("1 - Izbriši poruku");
                    var choice = Console.ReadLine();
                    int.TryParse(choice, out selectId);
                    if (choice == "0")
                        return;
                    switch (selectId.ToString())
                    {
                        case "1":
                            var confirmation = MainMenu.ConfirmationDialog();
                            if (!confirmation)
                                return;
                            var check = ResponseType.Success;
                            foreach (var item in message.AllEmails)
                            {
                                check = messageReceiversRepo.Delete(item, message.Id);
                            }
                            check = messageRepo.Delete(message.Id);
                            if (check != ResponseType.Success)
                            {
                                Console.WriteLine("Nije moguće izbrisati poruku");
                                Console.ReadLine();
                                return;
                            }

                            return;
                        default:
                            Console.WriteLine("Krivi upis");
                            Console.ReadLine();
                            break;
                    }
                }
            }
        }

    }
}