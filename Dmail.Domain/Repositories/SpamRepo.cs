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
        public ResponseType Delete(int blockerId, int blockedId)
        {
            var connectionToDelete = DbContext.Spam.Find(blockerId,blockedId);
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
            if (blockedId < 0 || blockedId > DbContext.Users.Count() + 1)
                return ResponseType.ValidationFailed;
            if (blockedId<0 || blockedId>DbContext.Users.Count()+1)
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
        public ICollection<MessagePrint> GetMessagesBySender(int receiverId, string  sender)
        {
            if (DbContext.Spam.Find(receiverId, DbContext.Users.FirstOrDefault(x => x.Email == sender).Id) == null)
                return new List<MessagePrint>();
            var messages = DbContext.MessagesReceivers.Where(x => x.ReceiverId == receiverId)
                .Join(DbContext.Messages, x => x.MessageId, m => m.Id, (x, m) => new { x, m })
                .Where(f => f.m.Sender.Email.Contains(sender) == true)
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
                    DateOfEvent= f.m.DateOfEvent,
                }).OrderByDescending(f => f.CreatedAt).ToList();
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
                    IsEvent= f.m.IsEvent,
                    SenderEmail = f.m.Sender.Email,
                    RecipientId = receiverId,
                    CreatedAt=f.m.CreatedAt,
                    AllEmails = f.m.MessagesReceivers.Where(x => x.MessageId == f.m.Id).Select(c => c.ReceiverId).ToList(),
                    DateOfEvent = f.m.DateOfEvent
                }).OrderByDescending(f => f.CreatedAt).ToList();
            foreach (var item in messages.ToList())
            {
                if (DbContext.Spam.Find(receiverId, item.SenderId) == null)
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
                    IsEvent= f.m.IsEvent,
                    SenderId = f.m.SenderId,
                    SenderEmail = f.m.Sender.Email,
                    RecipientId = receiverId,
                    AllEmails = f.m.MessagesReceivers.Where(x => x.MessageId == f.m.Id).Select(c => c.ReceiverId).ToList(),
                    DateOfEvent = f.m.DateOfEvent
                }).OrderByDescending(f => f.CreatedAt).ToList();
            foreach (var item in messages.ToList())
            {
                if (DbContext.Spam.Find(receiverId, item.SenderId) == null)
                    messages.Remove(item);
            }
            return messages;
        }
        
    }
}
