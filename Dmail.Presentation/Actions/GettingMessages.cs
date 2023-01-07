using Dmail.Domain.Models;
using Dmail.Domain.Repositories;
using Dmail.Presentation.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dmail.Presentation.Actions
{
    public class GettingMessages
    {
        public void GetSeenMessages(UserRepo userRepo, MessageRepo messageRepo, bool spam)
        {
            Console.WriteLine("Pročitane poruke");
            ICollection<MessagePrint> messages = null;
            if (!spam)
            {
                messages = messageRepo.GetSeenMessages(Info.UserId);
            }
            else
            {
                messages = Info.Repos.SpamRepo.GetSeenMessages(Info.UserId);
            }
            Console.Clear();
            if (messages.Count == 0)
            {
                Console.WriteLine("Nijedna poruka nije pronađena");
                Console.ReadLine();
                return;
            }
           IncomingMessageMenu.MessagesMenu(messages, 1);

        }
        public void GetNonSeenMessages(UserRepo userRepo, MessageRepo messageRepo, MessageReceiversRepo messageReceiversRepo, bool spam)
        {
            Console.Clear();
            Console.WriteLine("Nepročitane poruke");
            ICollection<MessagePrint> messages = null;
            if (!spam)
            {
                messages = messageRepo.GetNonSeenMessages(Info.UserId);
            }
            else
            {
                messages = Info.Repos.SpamRepo.GetNonSeenMessages(Info.UserId);
            };
            Console.Clear();
            if (messages.Count == 0)
            {
                Console.WriteLine("Nijedna poruka nije pronađena");
                Console.ReadLine();
                return;
            }
           IncomingMessageMenu.MessagesMenu(messages, 2);

        }
        public void GetMessagesbySender(UserRepo userRepo, MessageRepo messageRepo, bool spam)
        {
            var senderId = -1;
            Console.WriteLine("Poruke od specifičnog pošiljatelja");
            var sender = Console.ReadLine();
            ICollection<MessagePrint> messages;
            if (!spam)
            {
                messages = messageRepo.GetMessagesBySender(Info.UserId, sender);
            }
            else
            {
                messages = Info.Repos.SpamRepo.GetMessagesBySender(Info.UserId, sender);
            }
            if (messages.Count() == 0)
            {
                Console.WriteLine("Nije pronađena ni jedna poruka poslana od tog maila");
                Console.ReadLine();
                return;
            }
            Console.Clear();
           IncomingMessageMenu.MessagesMenu(messages, 2);

        }
        public ICollection<MessagePrint> GetSentMessages(MessageRepo messageRepo)
        {
            var messages = messageRepo.GetSentMessages(Info.UserId);
            return messages;
        }
    }
}
