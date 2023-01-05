using Dmail.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dmail.Domain.Factories;

namespace Dmail.Presentation.Menus
{
    public static class SpamMessageMenu
    {
        public static int _choice;
        public static void Content()
        {
            while (true)
            {
                _choice = -1;
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Spam menu");
                    Console.WriteLine("Izaberite opciju");
                    Console.WriteLine("1 - Pročitana pošta");
                    Console.WriteLine("2 - Nepročitana pošta");
                    Console.WriteLine("3 - Pošta određenoga pošiljatelja");
                    Console.WriteLine("0 - Povratak na glavni menu");
                    int.TryParse(Console.ReadLine(), out _choice);
                    if (_choice < 0)
                    {
                        Console.WriteLine("Nije upisan valjani input");
                        Console.ReadLine();
                    }
                    else if (_choice == 0)
                        return;
                    switch (_choice)
                    {
                        case 1:
                            IncomingMessageMenu.GetSeenMessages(MainMenu.userRepo, new MessageRepo(DmailDbContextFactory.GetDmailContext()), true);
                            break;
                        case 2:
                           IncomingMessageMenu.GetNonSeenMessages(MainMenu.userRepo, new MessageRepo(DmailDbContextFactory.GetDmailContext()), new MessageReceiversRepo(DmailDbContextFactory.GetDmailContext()), true);
                            break;
                        case 3:
                            IncomingMessageMenu.GetMessagesbySender(MainMenu.userRepo, new MessageRepo(DmailDbContextFactory.GetDmailContext()), true);
                            break;
                        default:
                            Console.WriteLine("Nije upisan točni input");
                            Console.ReadLine();
                            break;
                    }
                }

            }
        }
    }
}
