using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dmail.Data.Entities.Models
{
    public class Spam
    {
        public int MarkerId { get; set; }
        public int MarkedId;
        public bool IsSpam;
    }
}
