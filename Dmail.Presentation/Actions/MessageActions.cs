using Dmail.Data.Entities.Models;
using Dmail.Domain.Repositories;
using Dmail.Presentation.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dmail.Domain.Models;
using Dmail.Domain.Enums;

namespace Dmail.Presentation.Actions
{
    public class MessageActions
    {
        public void GetMessageAndUpdateSeenStatus(MessagePrint message, MessageReceiversRepo messageReceiversRepo)
        {
            messageReceiversRepo.Update(message.Id, Info.UserId, true);
            if (message.IsEvent)
                Prints.PrintDetailedEvent(message, messageReceiversRepo.GetStatus(Info.UserId, message.Id));
            else
                Prints.PrintDetailedMessage(message, false);
        }
        public ResponseType SetMessageAsUnseen(MessagePrint message, MessageReceiversRepo messageReceiversRepo, int userId)
        {
            var returnValue = messageReceiversRepo.Update(message.Id, userId, false);
            return returnValue;
        }
        public ResponseType DeleteMessageConnection(int messageId, int receiverId, MessageReceiversRepo messageReceiversRepo)
        {
            var returnValue = messageReceiversRepo.Delete(receiverId, messageId);
            return returnValue;
        }
        public ResponseType DeleteMessage(MessageRepo messageRepo, int messageId)
        {
            var returnValue = messageRepo.Delete(messageId);
            return returnValue;
        }
        public ResponseType AnswerEvent(MessageReceiversRepo messageReceiversRepo, int receiverId, int eventId, bool answer)
        {

            var returnValue = messageReceiversRepo.UpdateAnswerToEvent(eventId, receiverId, answer);
            return returnValue;
        }
        public int GenerateMessage(MessageRepo messageRepo, string title,  string answer)
        {
            var returnValue = messageRepo.NewMessage(Info.UserId, false, DateTime.MinValue.ToUniversalTime(), title, answer);
            return returnValue;
        }
        public int GenerateEvent(MessageRepo messageRepo, string title, DateTime dateOfEvent)
        {
            var returnValue = messageRepo.NewMessage(Info.UserId, true, dateOfEvent, title, "");
            return returnValue;
        }
        public ResponseType MakeConnection(MessageReceiversRepo messageReceiversRepo, int messageId, int receiverId)
        {
            var returnValue = messageReceiversRepo.NewConnection(messageId, receiverId);
            return returnValue;
        }
    }
}
