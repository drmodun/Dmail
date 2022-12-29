namespace Dmail.Data.Entities.Models
{
    public class Event
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public User? Sender;
        public string Title { get; set; }
        public DateTime DateOfEvent { get; set; }
        public int Status { get; set; }
        public ICollection<EventsUsers> EventUsers { get; set; } = new List<EventsUsers>();

    }

}
