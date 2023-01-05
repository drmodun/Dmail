using Dmail.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dmail.Presentation.Menus
{
    public static class OutgoingMessageMenu
    {
        public static void GetSentMessages(UserRepo userRepo, MessageRepo messageRepo)
        {
            Console.WriteLine("Poslane poruke");
            Console.WriteLine("Upišite broj poruke koje želite urediti");
            var iterator = 1;
            var messages = messageRepo.GetSentMessages(AccountMenus.UserId);
            foreach (var message in messages)
            {
                Console.WriteLine(iterator.ToString());
                Prints.PrintMessage(message);
                Console.WriteLine(" ");
            }
            Console.ReadLine();
        }
    }
}
