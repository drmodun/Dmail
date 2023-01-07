using Dmail.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dmail.Domain.Factories;
using Dmail.Presentation.Actions;

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
                    var actions = new GettingMessages();
                    Console.Clear();
                    Console.WriteLine("Spam menu");
                    Console.WriteLine("Izaberite opciju");
                    Console.WriteLine("1 - Pročitana pošta");
                    Console.WriteLine("2 - Nepročitana pošta");
                    Console.WriteLine("3 - Pošta određenoga pošiljatelja");
                    Console.WriteLine("0 - Povratak na glavni menu");
                    var choice = Console.ReadLine();
                    int.TryParse(choice, out _choice);
                    if (_choice == 0 && choice!="0")
                    {
                        Console.WriteLine("Nije upisan valjani input");
                        Console.ReadLine();
                        continue;
                    }
                    switch (_choice)
                    {
                        case 1:
                            actions.GetSeenMessages(Info.Repos.UserRepo, Info.Repos.MessageRepo, true);
                            break;
                        case 2:
                           actions.GetNonSeenMessages(Info.Repos.UserRepo, Info.Repos.MessageRepo, Info.Repos.MessageReceiversRepo, true);
                            break;
                        case 3:
                            actions.GetMessagesbySender(Info.Repos.UserRepo, Info.Repos.MessageRepo, true);
                            break;
                        case 0:
                            return;
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
