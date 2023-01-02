using Dmail.Data.Entities;
using Dmail.Data.Entities.Models;
using Dmail.Domain.Enums;
using Microsoft.EntityFrameworkCore.Metadata;
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
           if (DbContext.Events.Find(eventToAdd.Id)!=null)
                return ResponseType.Exists;
           var sender = DbContext.Users.Find(eventToAdd.SenderId);
           if (sender == null)
                return ResponseType.NotFound;
           if (eventToAdd.DateOfEvent < DateTime.Now || eventToAdd.Title.Length==0)
                return ResponseType.ValidationFailed;
           DbContext.Events.Add(eventToAdd);
            return SaveChanges();

        }
        public ResponseType Delete(int id)
        {
            var eventToDelete = DbContext.Events.Find(id);
            if (eventToDelete == null)
                return ResponseType.NotFound;
            DbContext.Events.Remove(eventToDelete);
            return SaveChanges();
        }
        //Add updates later
    }
}
