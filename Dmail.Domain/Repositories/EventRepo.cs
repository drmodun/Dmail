using Dmail.Data.Entities;
using Dmail.Data.Entities.Models;
using Dmail.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dmail.Domain.Repositories
{
    public class EventRepo : BaseRepo
    {
        public EventRepo(DmailContext DbContext) : base(DbContext)
        {

        }
        public ResponseType Add(Event eventToAdd)
        {
            
        }
    }
}
