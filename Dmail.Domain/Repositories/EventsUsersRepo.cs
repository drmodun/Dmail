using Dmail.Data.Entities;
using Dmail.Data.Entities.Models;
using Dmail.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dmail.Domain.Repositories
{
    public class EventsUsersRepo : BaseRepo
    {
        public EventsUsersRepo(DmailContext DbContext) : base(DbContext)
        {

        }
        public ResponseType Add(EventsUsers connection)
        {
            if (DbContext.Events.Find(connection.EventId)==null)
                return ResponseType.NotFound;
            if (DbContext.Users.Find(connection.UserId) == null)
                return ResponseType.NotFound;
            if (DbContext.EventUsers.Find(new {connection.EventId, connection.UserId}) != null)
                return ResponseType.Exists;
            DbContext.EventUsers.Add(connection);
            return SaveChanges();
        }
        public ResponseType Delete(int id)
        {
            var connectionToRemove = DbContext.EventUsers.Find(id);
            if (connectionToRemove == null)
                return ResponseType.NotFound;
            DbContext.EventUsers.Remove(connectionToRemove);
            return SaveChanges();
        }
        public ResponseType Update (int id, bool accepted)
        {
            var connectionToUpdate = DbContext.EventUsers.Find(id);
            if (connectionToUpdate == null)
                return ResponseType.NotFound;
            if (connectionToUpdate.Accepted==accepted)
                return ResponseType.NotChanged;
            connectionToUpdate.Accepted = accepted;
            return SaveChanges();
        }
    }
}
