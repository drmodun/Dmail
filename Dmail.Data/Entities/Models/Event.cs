namespace Dmail.Data.Entities.Models
{
    public class Event : ToSend
    {
        public Event() : base() { }
        public DateTime DateOfEvent { get; set; }
        public int Status { get; set; }
        public ICollection<EventsUsers> EventUsers { get; set; } = new List<EventsUsers>();

    }

}
