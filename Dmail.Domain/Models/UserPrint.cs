using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dmail.Domain.Models
{
    public class UserPrint
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public bool Blocked { get; set; }

    }
}
