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
            Console.WriteLine(messageToPrint.Title);
            Console.WriteLine(messageToPrint.Body);
            Console.WriteLine(messageToPrint.SenderEmail);
            Console.WriteLine(messageToPrint.RecipientId);
        }
    }
}
