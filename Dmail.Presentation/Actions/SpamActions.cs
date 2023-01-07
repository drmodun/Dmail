using Dmail.Domain.Enums;
using Dmail.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dmail.Presentation.Actions
{
    public class SpamActions
    {
        public ResponseType MakeSpamConnection(int blockerId, int blockedId, SpamRepo spamRepo)
        {
            var returnValue = spamRepo.TryAdd(blockerId, blockedId);
            return returnValue;
        }
        public ResponseType RemoveSpamConnection(int unblockerId, int unblocked, SpamRepo spamRepo)
        {
            var returnValue = spamRepo.Delete(unblockerId, unblocked);
            return returnValue;
        }
    }
}
