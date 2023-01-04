using Dmail.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dmail.Presentation.Menus
{
    public static class Prints
    {
        public static void PrintMessage(MessagePrint messageToPrint)
        {
            Console.WriteLine("Naslov: "+messageToPrint.Title);
            Console.WriteLine("Pošiljatelj: " + messageToPrint.SenderEmail);
        }
        public static void PrintDetailedMessage(MessagePrint messageToPrint)
        {
            Console.WriteLine("Naslov: "+messageToPrint.Title);
            Console.WriteLine("Poruka: "+messageToPrint.Body);
            Console.WriteLine("Poslano na: "+messageToPrint.CreatedAt);
            Console.WriteLine("Pošiljatelj: "+messageToPrint.SenderEmail);
            Console.WriteLine("Primatelj: "+messageToPrint.RecipientId);
        }
        public static void PrintAsSender(MessagePrint messageToPrint)
        {
            Console.WriteLine("Naslov: "+messageToPrint.Title);
            Console.WriteLine("Primatelji: "+string.Join(", ",messageToPrint.AllEmails));
//
        }
    }
}
