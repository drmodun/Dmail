using Dmail.Domain.Models;
using Dmail.Domain.Repositories;

namespace Dmail.Presentation.Actions
{
    public class GetUsers
    {
        public ICollection<UserPrint> GetSenderUsers(int receiverId, MessageRepo messageRepo)
        {
            var returnValue = messageRepo.GetSenderUsers(receiverId);
            return returnValue;
        }
        public ICollection<UserPrint> GetReceiverUsers(int senderId, MessageRepo messageRepo)
        {
            var returnValue = messageRepo.GetReceiverUsers(senderId);
            return returnValue;
        }
    }
}
