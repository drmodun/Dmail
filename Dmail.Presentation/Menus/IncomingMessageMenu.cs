using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dmail.Domain.Repositories;
using Dmail.Presentation;
namespace Dmail.Presentation.Menus
{
    public class IncomingMessageMenu
    {
        private static int _choice;

        public static void Content()
        {
            _choice = -1;
            while (true)
            {
                Console.Clear();
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
                        break;

                }
            }
            
        }
        public static void SeenMessages(UserRepo userRepo, MessageRepo messageRepo)
        {
           
        }
    }
}
//questions
/*how many seed data tests
 * are events and emails supposed to be two diffwereent tsbles
 * how much linq do we have to use
 * how much abstraction muzsta we do on presentation layer
 * can I use stratic classesd fgor menus and stuff
 * do we have to use interfaces and stuff like that*/