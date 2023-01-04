
namespace Dmail.Data.Entities.Models
{
    public class Message : ToSend
    {
        public Message() : base(){ }
        public ICollection<MessagesReceivers> MessagesReceivers { get; set; } = new List<MessagesReceivers>();

    }
}
