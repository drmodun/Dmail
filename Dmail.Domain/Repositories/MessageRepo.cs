using Dmail.Data.Entities;
using Dmail.Data.Entities.Models;
using Dmail.Domain.Enums;
using Dmail.Domain.Factories;
using Dmail.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
namespace Dmail.Domain.Repositories
{
    public class MessageRepo : BaseRepo
    {
        public Message? GetMessage(int id) => DbContext.Messages.Find(id);
        public ICollection<Message> GetMessages() => DbContext.Messages.ToList();

        public MessageRepo(DmailContext dbContext) : base(dbContext)
        {

        }
        public ResponseType Add(Message message)
        {
            if (message.Title.Length == 0)
                return ResponseType.ValidationFailed;
            if (DbContext.Messages.Find(message.Id) != null)
                return ResponseType.Exists;
            DbContext.Messages.Add(message);
            return SaveChanges();

        }

        public ResponseType Delete(int message)
        {
            var messageToDelete = DbContext.Messages.Find(message);
            if (messageToDelete == null)
                return ResponseType.NotFound;
            DbContext.Messages.Remove(messageToDelete);
            return SaveChanges();
        }

        public ICollection<MessagePrint> GetSeenMessages(int receiverId)
        {
            var messages = DbContext.MessagesReceivers.Where(x => x.ReceiverId == receiverId).
                Where(x => x.Read == true)
                .Join(DbContext.Messages, x => x.MessageId, m => m.Id, (x, m) => new { x, m }).
                Select(f => new MessagePrint()
                {
                    Id = f.m.Id,
                    Title = f.m.Title,
                    Body = f.m.Body,
                    SenderId = f.m.SenderId,
                    SenderEmail = f.m.Sender.Email,
                    RecipientId = receiverId,
                    RecipientEmail = DbContext.Users.Find(receiverId).Email,
                    AllEmails = f.m.MessagesReceivers.Where(x => x.MessageId == f.m.Id).Select(c => c.ReceiverId).ToList(),
                    DateOfEvent = f.m.DateOfEvent
                }).ToList();
            
            foreach (var item in messages)
            {
                if (DbContext.Spam.Find(receiverId, item.SenderId) != null)
                    messages.Remove(item);
            }
            messages.OrderBy(x => x.CreatedAt).ToList();
            return messages;
        }

        public ICollection<MessagePrint> GetNonSeenMessages(int receiverId)
        {
            var messages = DbContext.MessagesReceivers.Where(x => x.ReceiverId == receiverId).
                Where(x => x.Read == false)
                .Join(DbContext.Messages, x => x.MessageId, m => m.Id, (x, m) => new { x, m }).
                Select(f => new MessagePrint()
                {
                    Id = f.m.Id,
                    Title = f.m.Title,
                    Body = f.m.Body,
                    SenderId = f.m.SenderId,
                    SenderEmail = f.m.Sender.Email,
                    RecipientId = receiverId,
                    IsEvent=f.m.IsEvent,
                    RecipientEmail = DbContext.Users.Find(receiverId).Email,
                    AllEmails = f.m.MessagesReceivers.Where(x => x.MessageId == f.m.Id).Select(c => c.ReceiverId).ToList(),
                    DateOfEvent=f.m.DateOfEvent
                }).ToList();
            messages.OrderBy(x => x.CreatedAt).ToList();
            foreach (var item in messages)
            {
                if (DbContext.Spam.Find(receiverId, item.SenderId) != null)
                    messages.Remove(item);
            }
            return messages;
        }
        public ICollection<MessagePrint> GetSentMessages(int senderId)
        {
            var messages = DbContext.Messages.Where(x => x.SenderId == senderId)
                    .Select(f => new MessagePrint()
                    {
                        Id = f.Id,
                        Title = f.Title,
                        Body = f.Body,
                        SenderId = f.SenderId,
                        SenderEmail = f.Sender.Email,
                        AllEmails = f.MessagesReceivers.Where(x => x.MessageId == f.Id).Select(c => c.ReceiverId).ToList(),
                        IsEvent=f.IsEvent,
                        DateOfEvent=f.DateOfEvent
                    }).ToList();
            messages.OrderBy(x => x.CreatedAt).ToList();
            return messages;
        }
        public ICollection<MessagePrint> GetMessagesBySender(int receiverId, string sender)
        {
            var messages = DbContext.MessagesReceivers.Where(x => x.ReceiverId == receiverId)
                .Join(DbContext.Messages, x => x.MessageId, m => m.Id, (x, m) => new { x, m })
                .Where(f=>f.m.Sender.Email.Contains(sender)==true)
                .Select(f => new MessagePrint()
                {
                    Id = f.m.Id,
                    Title = f.m.Title,
                    Body = f.m.Body,
                    SenderId = f.m.SenderId,
                    SenderEmail = f.m.Sender.Email,
                    AllEmails = f.m.MessagesReceivers.Where(n => n.MessageId == f.m.Id).Select(c => c.ReceiverId).ToList(),
                    RecipientId = receiverId,
                    RecipientEmail = DbContext.Users.Find(receiverId).Email,
                    IsEvent = f.m.IsEvent,
                    CreatedAt= f.m.CreatedAt,
                    DateOfEvent=f.m.DateOfEvent
                }).ToList();
            foreach (var item in messages)
            {
                if (DbContext.Spam.Find(receiverId, item.SenderId) != null)
                    messages.Remove(item);
            }
            messages.OrderBy(d=>d.CreatedAt).ToList();
            return messages;

        }
        public int NewMessage(int senderId, bool isEvent,  DateTime dateOfEvent, string title, string body)
        {
            var message = new Message()
            {
                Title = title,
                Body = body,
                SenderId = senderId,
                CreatedAt = DateTime.UtcNow,
                IsEvent= isEvent,
            };
            if (isEvent)
                message.DateOfEvent= dateOfEvent;
            var check =Add(message);
            if (check!=ResponseType.Success)
            {
                Console.WriteLine(check.ToString());
                return -1;
            }
            return message.Id;

        }
        
    }
}
