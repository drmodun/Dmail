using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
