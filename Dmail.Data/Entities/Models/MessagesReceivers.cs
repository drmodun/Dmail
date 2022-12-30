namespace Dmail.Data.Entities.Models
{
    public class MessagesReceivers
    {
        public int MessageId;
        public int ReceiverId;
        public Message? Message;
        public User? Receiver;
        public bool Read;
    }
}
