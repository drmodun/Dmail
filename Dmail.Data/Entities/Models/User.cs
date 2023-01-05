namespace Dmail.Data.Entities.Models
{
    public  class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string _password { get; set;}
        public User(string email, string password) {

               Email = email;
            _password = password;
        }

        public ICollection<Spam> Spams = new List<Spam>();
        public ICollection<Message> Messages { get; set; } = new List<Message>();
        public ICollection<MessagesReceivers> MessagesReceivers { get; set; } = new List<MessagesReceivers>();

    }
}
