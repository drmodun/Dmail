using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dmail.Data.Entities
{
    public class DmailContext : DbContext 
    {
        public DmailContext(DbContextOptions options) : base(options)
        {
        }

    }
}
