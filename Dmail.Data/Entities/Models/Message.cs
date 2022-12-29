
namespace Dmail.Data.Entities.Models
{
    public class Message
    {
        public int Id { get; set; }
        public int SenderId;
        public int ReceiverId;

        public User? Sender;
        public User? Receiver;
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
