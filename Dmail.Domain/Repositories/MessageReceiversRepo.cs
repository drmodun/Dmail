using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dmail.Data.Entities;
using Dmail.Data.Entities.Models;
using Dmail.Domain.Enums;

namespace Dmail.Domain.Repositories
{
    public class MessageReceiversRepo : BaseRepo
    {
        public MessageReceiversRepo(DmailContext dbContext) : base(dbContext)
        {
        }
        public ResponseType Add(MessagesReceivers messageReceivers)
        {
            var receiver = DbContext.Users.Find(messageReceivers.ReceiverId);
            var message = DbContext.Messages.Find(messageReceivers.MessageId);
            if (message == null || receiver == null)
                return ResponseType.NotFound;
            if (receiver.Id == message.SenderId)
                return ResponseType.ValidationFailed;
            DbContext.MessagesReceivers.Add(messageReceivers);
            return SaveChanges();
        }
        public ResponseType Delete(int messageId, int receiverId)
        {
            var receiver = DbContext.Users.Find(receiverId);
            var message = DbContext.Messages.Find(messageId);
            if (message == null || receiver == null)
                return ResponseType.NotFound;
            var connectionToRemove = DbContext.MessagesReceivers.Find(new { receiver, messageId });
            DbContext.MessagesReceivers.Remove(connectionToRemove);
            return SaveChanges();

        }
        public ResponseType Update(int messageId, int receiverId)
        {
            var receiver = DbContext.Users.Find(receiverId);
            var message = DbContext.Messages.Find(messageId);
            if (message == null || receiver == null)
                return ResponseType.NotFound;
            var connectionToUpdate = DbContext.MessagesReceivers.Find(new { receiver, messageId });
            if (connectionToUpdate.Read)
                return ResponseType.NotChanged;
            connectionToUpdate.Read = true;
            return SaveChanges();
        }
    }
}
