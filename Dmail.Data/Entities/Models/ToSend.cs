namespace Dmail.Data.Entities.Models
{
    public abstract class ToSend
    {
        public int Id { get; set; }
        public int SenderId;

        public User? Sender;
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsEvent = false;
        public DateTime DateOfEvent = DateTime.MinValue;

    }
}
