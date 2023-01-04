using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dmail.Data.Entities.Models
{
    public class EventsUsers
    {
        public int EventId { get; set; }
        public User User { get; set; }
        public Event Event{ get; set; }
        public int UserId { get; set; }
        public bool Read;
        public bool Accepted ;
    }
}
