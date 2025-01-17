﻿using Dmail.Data.Entities;
using Dmail.Data.Entities.Models;
using Dmail.Domain.Enums;
using Dmail.Domain.Models;
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
                    CreatedAt = f.m.CreatedAt,
                    IsEvent = f.m.IsEvent,
                    AllEmails = f.m.MessagesReceivers.Where(x => x.MessageId == f.m.Id).Select(c => c.ReceiverId).ToList(),
                    DateOfEvent = f.m.DateOfEvent
                }).OrderByDescending(f => f.CreatedAt).ToList();

            foreach (var item in messages.ToList())
            {
                if (DbContext.Spam.Find(receiverId, item.SenderId) != null)
                    messages.Remove(item);
            }
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
                    IsEvent = f.m.IsEvent,
                    CreatedAt = f.m.CreatedAt,
                    AllEmails = f.m.MessagesReceivers.Where(x => x.MessageId == f.m.Id).Select(c => c.ReceiverId).ToList(),
                    DateOfEvent = f.m.DateOfEvent
                }).OrderByDescending(f => f.CreatedAt).ToList();
            foreach (var item in messages.ToList())
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
                        CreatedAt = f.CreatedAt,
                        AllEmails = f.MessagesReceivers.Where(x => x.MessageId == f.Id).Select(c => c.ReceiverId).ToList(),
                        IsEvent = f.IsEvent,
                        DateOfEvent = f.DateOfEvent
                    }).OrderByDescending(f => f.CreatedAt).ToList();
            return messages;
        }
        public ICollection<MessagePrint> GetMessagesBySender(int receiverId, string sender)
        {
            var messages = DbContext.MessagesReceivers.Where(x => x.ReceiverId == receiverId)
                .Join(DbContext.Messages, x => x.MessageId, m => m.Id, (x, m) => new { x, m })
                .Where(f => f.m.Sender.Email.Contains(sender) == true && f.m.SenderId != receiverId)
                .Select(f => new MessagePrint()
                {
                    Id = f.m.Id,
                    Title = f.m.Title,
                    Body = f.m.Body,
                    SenderId = f.m.SenderId,
                    SenderEmail = f.m.Sender.Email,
                    AllEmails = f.m.MessagesReceivers.Where(n => n.MessageId == f.m.Id).Select(c => c.ReceiverId).ToList(),
                    RecipientId = receiverId,
                    IsEvent = f.m.IsEvent,
                    CreatedAt = f.m.CreatedAt,
                    DateOfEvent = f.m.DateOfEvent
                }).OrderByDescending(f => f.CreatedAt).ToList();
            foreach (var item in messages.ToList())
            {
                if (DbContext.Spam.Find(receiverId, item.SenderId) != null)
                    messages.Remove(item);
            }
            return messages;

        }
        public int NewMessage(int senderId, bool isEvent, DateTime dateOfEvent, string title, string body)
        {
            var message = new Message()
            {
                Title = title,
                Body = body,
                SenderId = senderId,
                CreatedAt = DateTime.Now.ToUniversalTime(),
                IsEvent = isEvent,
            };
            if (isEvent) { message.DateOfEvent = dateOfEvent; }
            var check = Add(message);
            if (check != ResponseType.Success)
            {
                Console.WriteLine(check.ToString());
                return -1;
            }
            return message.Id;

        }
        public ICollection<UserPrint> GetSenderUsers(int receiverId)
        {
            var users = DbContext.MessagesReceivers.Where(x => x.ReceiverId == receiverId)
                .Join(DbContext.Messages, x => x.MessageId, y => y.Id, (x, y) => new { x, y })
                .Select(f => new UserPrint()
                {
                    Id = f.y.SenderId,
                    Email = f.y.Sender.Email,
                    Blocked = DbContext.Spam.FirstOrDefault(x => x.BlockerId == receiverId && x.Blocked == f.y.SenderId) != default
                }).Distinct().ToList();
            var removeSelf = users.FirstOrDefault(x => x.Id == receiverId);
            if (removeSelf != null)
                users.Remove(removeSelf);
            return users;
        }

        public ICollection<UserPrint> GetReceiverUsers(int senderId)
        {
            var users = DbContext.Messages.Where(x => x.SenderId == senderId)
                .Join(DbContext.MessagesReceivers, x => x.Id, y => y.MessageId, (x, y) => new { x, y })
                .Select(f => new UserPrint()
                {
                    Id = f.y.ReceiverId,
                    Email = f.y.Receiver.Email,
                    Blocked = DbContext.Spam.FirstOrDefault(x => x.BlockerId == senderId && x.Blocked == f.y.ReceiverId) != default
                }).Distinct().ToList();
            var removeSelf = users.FirstOrDefault(x => x.Id == senderId);
            if (removeSelf != null)
                users.Remove(removeSelf);
            return users;
        }

    }
}
