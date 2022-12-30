using Dmail.Data.Entities;
using Dmail.Data.Entities.Models;
using Dmail.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
namespace Dmail.Domain.Repositories
{
    public  class MessageRepo : BaseRepo
    {
        public MessageRepo(DmailContext dbContext) : base(dbContext)
        {

        }
        public ResponseType Add(Message message)
        {
            var connectionManager = new MessageReceiversRepo(DbContext);
            var senderId=DbContext.Users.Find(message.SenderId);
            if (message.Title.Length == 0 || message.Body.Length == 0 || message.MessagesReceivers.Count==0)
                return ResponseType.ValidationFailed;
            if (senderId==null)
                return ResponseType.NotFound;
            var iteration = 0;
            foreach (var item in message.MessagesReceivers)
            {

                var response = connectionManager.Add(item);
                if (response == ResponseType.NotFound || response == ResponseType.ValidationFailed)
                {
                    for (var i=0; i<iteration; i++)
                    {
                        var remove = DbContext.MessagesReceivers.Find(DbContext.MessagesReceivers.Count()-1-i);
                        DbContext.MessagesReceivers.Remove(remove);
                    }
                    return ResponseType.ValidationFailed;
                }
                iteration++;
            }
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
        

    }
}
