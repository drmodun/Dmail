using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dmail.Domain.Models
{
    public class MessagePrint
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int SenderId { get; set; }
        public string SenderEmail { get; set;}
        public int RecipientId;
        public string RecipientEmail { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsEvent { get; set; }
        public DateTime DateOfEvent;
        public ICollection<int> AllEmails { get; set; }
    }
}
