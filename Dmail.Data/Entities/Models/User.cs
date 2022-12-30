namespace Dmail.Data.Entities.Models
{
    public  class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string _password { get; init;}

        public ICollection<Spam> Spams = new List<Spam>();
        public ICollection<EventsUsers> EventUsers { get; set; } = new List<EventsUsers>();
        public ICollection<Event> Events { get; set; } = new List<Event>();
        public ICollection<Message> Messages { get; set; } = new List<Message>();
        public ICollection<MessagesReceivers> MessagesReceivers { get; set; } = new List<MessagesReceivers>();

    }
}
