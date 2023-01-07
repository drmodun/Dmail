using Dmail.Data.Enums;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Dmail.Data.Entities.Models
{
    public class MessagesReceivers
    {
        public int MessageId;
        public int ReceiverId;
        public Message? Message;
        public User? Receiver;
        public EventAnswer Answer;
        public bool Read = false;
    }
}
