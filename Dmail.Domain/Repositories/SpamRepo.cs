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
            DbContext.Spam.Add(spam);
            return SaveChanges();
        }
        public ResponseType Delete(int blockerid, int blockedId)
        {
            var connectionToDelete = DbContext.Spam.Find(blockedId,blockedId);
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
        public ResponseType TryAdd(int blockerId, int blockedId)
        {
            if (blockedId < 0 || blockedId > DbContext.Spam.Count() + 1)
                return ResponseType.ValidationFailed;
            if (blockedId<0 || blockedId>DbContext.Spam.Count()+1)
                return ResponseType.ValidationFailed;
            var spam = new Spam()
            {
                BlockerId = blockerId,
                Blocked = blockedId,
                IsBlocked = true
            };
            if (DbContext.Spam.Find(blockerId, blockedId) != null)
                return ResponseType.Exists;
            Add(spam);
            return ResponseType.Success;
        }
    }
}
