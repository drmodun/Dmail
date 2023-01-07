namespace Dmail.Data.Entities.Models
{
    public class Spam
    {
        public int BlockerId { get; set; }
        public User? Blocker;
        public int Blocked;
        public bool IsBlocked = true;
    }
}
