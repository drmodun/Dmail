using Dmail.Data.Entities;
using Dmail.Data.Entities.Models;
using Dmail.Domain.Enums;
using Dmail.Domain.Models;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Dmail.Domain.Repositories
{
    public class SpamRepo : BaseRepo
    {
        public SpamRepo(DmailContext DbContext) : base(DbContext)
        {

        }
        public ResponseType Add(Spam spam)
        {
            var blocker = DbContext.Users.Find(spam.BlockerId);
            if (blocker == null)
                return ResponseType.NotFound;
            var blocked = DbContext.Users.Find(spam.Blocked);
            if (blocked == null)
                return ResponseType.NotFound;
            DbContext.Spam.Add(spam);
            return SaveChanges();
        }
        public ResponseType Delete(int blockerid, int blockedId)
        {
            var connectionToDelete = DbContext.Spam.Find(blockedId,blockedId);
            if (connectionToDelete == null)
                return ResponseType.NotFound;
            DbContext.Spam.Remove(connectionToDelete);
            return SaveChanges();

        }
        public ResponseType Update(int id, bool blocked)
        {
            var connectionToUpdate = DbContext.Spam.Find(id);
            if (connectionToUpdate == null)
                return ResponseType.NotFound;
            if (connectionToUpdate.IsBlocked==blocked)
                return ResponseType.NotChanged;
            connectionToUpdate.IsBlocked = blocked;
            return SaveChanges();
        }
        public ResponseType TryAdd(int blockerId, int blockedId)
        {
            if (blockedId < 0 || blockedId > DbContext.Spam.Count() + 1)
                return ResponseType.ValidationFailed;
            if (blockedId<0 || blockedId>DbContext.Spam.Count()+1)
                return ResponseType.ValidationFailed;
            var spam = new Spam()
            {
                BlockerId = blockerId,
                Blocked = blockedId,
                IsBlocked = true
            };
            if (DbContext.Spam.Find(blockerId, blockedId) != null)
                return ResponseType.Exists;
            Add(spam);
            return ResponseType.Success;
        }
        public ICollection<MessagePrint> GetMessagesBySender(int receiverId, int senderId)
        {
            var messages = DbContext.MessagesReceivers.Where(x => x.ReceiverId == receiverId)
                .Join(DbContext.Messages, x => x.MessageId, m => m.Id, (x, m) => new { x, m })
                .Where(f => f.m.SenderId == senderId)
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
                    IsEvent = false,
                    CreatedAt = f.m.CreatedAt
                }).ToList();
            var events = DbContext.EventUsers.Where(x => x.UserId == receiverId)
                .Join(DbContext.Events, x => x.EventId, e => e.Id, (x, e) => new { x, e })
                .Where(g => g.e.SenderId == senderId)
                .Select(f => new MessagePrint()
                {
                    Id = f.e.Id,
                    Title = f.e.Title,
                    Body = f.e.Body,
                    SenderId = f.e.SenderId,
                    SenderEmail = f.e.Sender.Email,
                    AllEmails = f.e.EventUsers.Where(x => x.EventId == f.e.Id).Select(c => c.UserId).ToList(),
                    IsEvent = true,
                    DateOfEvent = f.e.DateOfEvent
                }).ToList();
            foreach (var e in events)
            {
                messages.Add(e);
            }
            foreach (var item in messages)
            {
                if (DbContext.Spam.Find(receiverId, item.SenderId) == null)
                    messages.Remove(item);
            }
            messages.OrderBy(d => d.CreatedAt).ToList();
            return messages;

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
                    RecipientEmail = DbContext.Users.Find(receiverId).Email
                }).ToList();
            var events = DbContext.EventUsers.Where(x => x.UserId == receiverId)
                .Where(x => x.Read == true)
                .Join(DbContext.Events, x => x.EventId, e => e.Id, (x, e) => new { x, e })
                .Select(f => new MessagePrint()
                {
                    Id = f.e.Id,
                    Title = f.e.Title,
                    Body = f.e.Body,
                    SenderId = f.e.SenderId,
                    SenderEmail = f.e.Sender.Email,
                    AllEmails = f.e.EventUsers.Where(x => x.EventId == f.e.Id).Select(c => c.UserId).ToList(),
                    IsEvent = true,
                    DateOfEvent = f.e.DateOfEvent
                }).ToList();
            foreach (var e in events)
            {
                messages.Add(e);
            }
            foreach (var item in messages)
            {
                if (DbContext.Spam.Find(receiverId, item.SenderId) == null)
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
                    RecipientEmail = DbContext.Users.Find(receiverId).Email
                }).ToList();
            var events = DbContext.EventUsers.Where(x => x.UserId == receiverId)
                .Where(x => x.Read == false)
                .Join(DbContext.Events, x => x.EventId, e => e.Id, (x, e) => new { x, e })
                .Select(f => new MessagePrint()
                {
                    Id = f.e.Id,
                    Title = f.e.Title,
                    Body = f.e.Body,
                    SenderId = f.e.SenderId,
                    SenderEmail = f.e.Sender.Email,
                    AllEmails = f.e.EventUsers.Where(x => x.EventId == f.e.Id).Select(c => c.UserId).ToList(),
                    IsEvent = true,
                    DateOfEvent = f.e.DateOfEvent
                }).ToList();
            foreach (var e in events)
            {
                messages.Add(e);
            }
            messages.OrderBy(x => x.CreatedAt).ToList();
            foreach (var item in messages)
            {
                if (DbContext.Spam.Find(receiverId, item.SenderId) == null)
                    messages.Remove(item);
            }
            return messages;
        }
        
    }
}
