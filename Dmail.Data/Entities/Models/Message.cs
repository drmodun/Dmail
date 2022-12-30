
namespace Dmail.Data.Entities.Models
{
    public class Message
    {
        public int Id { get; set; }
        public int SenderId;

        public User? Sender;
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<MessagesReceivers> MessagesReceivers { get; set; } = new List<MessagesReceivers>();

    }
}
