using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Dmail.Presentation.Menus
{
    public static class NewMessageMenu
    {
        public static int _choice;
        public static void Content()
        {
            while (true)
            {
                _choice = -1;
                Console.Clear();
                Console.WriteLine("Nova pošta");
                Console.WriteLine("Izaberite opciju");
                Console.WriteLine("1 - Nova pošta");
                Console.WriteLine("0 - Main Menu");
                int.TryParse(Console.ReadLine(), out _choice);
                if (_choice<0 || _choice > 1)
                {
                    Console.WriteLine("Nije upisan valjani broj");
                    Console.ReadLine();
                }
                if (_choice == 0)
                    return;
                //new message system
            }
        }
    }
}
