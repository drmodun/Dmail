using Dmail.Data.Entities;
using Dmail.Data.Entities.Models;
using Dmail.Domain.Enums;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Dmail.Domain.Repositories
{
    public class SpamRepo : BaseRepo
    {
        public SpamRepo(DmailContext DbContext) : base(DbContext)
        {

        }
        public ResponseType Add(Spam spam)
        {
            var blocker = DbContext.Users.Find(spam.BlockerId);
            if (blocker == null)
                return ResponseType.NotFound;
            var blocked = DbContext.Users.Find(spam.Blocked);
            if (blocked == null)
                return ResponseType.NotFound;
            return SaveChanges();
        }
        public ResponseType Delete(int id)
        {
            var connectionToDelete = DbContext.Spam.Find(id);
            if (connectionToDelete == null)
                return ResponseType.NotFound;
            DbContext.Spam.Remove(connectionToDelete);
            return SaveChanges();

        }
        public ResponseType Update(int id, bool blocked)
        {
            var connectionToUpdate = DbContext.Spam.Find(id);
            if (connectionToUpdate == null)
                return ResponseType.NotFound;
            if (connectionToUpdate.IsBlocked==blocked)
                return ResponseType.NotChanged;
            connectionToUpdate.IsBlocked = blocked;
            return SaveChanges();
        }
    }
}
