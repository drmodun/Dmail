using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dmail.Data.Entities.Models
{
    public class MessagesReceivers
    {
        public int MessageId;
        public int ReceiverId;
        public Message? Message;
        public User? Receiver;
    }
}
