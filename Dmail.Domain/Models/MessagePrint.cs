namespace Dmail.Domain.Models
{
    public class MessagePrint
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int SenderId { get; set; }
        public string SenderEmail { get; set; }

        public int RecipientId;
        public DateTime CreatedAt { get; set; }
        public bool IsEvent { get; set; }
        public DateTime DateOfEvent;
        public ICollection<int> AllEmails { get; set; }
    }
}
