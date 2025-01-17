﻿using Dmail.Domain.Models;
using Dmail.Presentation.Actions;

namespace Dmail.Presentation.Menus
{
    public static class Prints
    {
        public static List<string> TranslatedEmails(ICollection<int> AllEmails)
        {
            var actions = new AccounActions();
            var list = new List<String>();
            foreach (var email in AllEmails)
            {
                list.Add(actions.GetUserEmail(email));
            }
            return list;

        }
        public static void PrintDetailedMessage(MessagePrint messageToPrint, bool sender)
        {
            Console.Clear();
            Console.WriteLine("Naslov: " + messageToPrint.Title);
            Console.WriteLine("Poruka: " + messageToPrint.Body);
            Console.WriteLine("Poslano na: " + messageToPrint.CreatedAt.AddHours(1));
            Console.WriteLine("Pošiljatelj: " + messageToPrint.SenderEmail);
            if (sender)
            {
                var emails = TranslatedEmails(messageToPrint.AllEmails);
                Console.WriteLine("Primatelji: " + string.Join(", ", emails));
            }
        }
        public static void PrintDetailedEvent(MessagePrint messageToPrint, bool accepted)
        {
            Console.Clear();
            Console.WriteLine("Naslov događaja: " + messageToPrint.Title);
            Console.WriteLine("Događa se u: " + messageToPrint.DateOfEvent.AddHours(1));
            Console.WriteLine("Pošiljatelj: " + messageToPrint.SenderEmail);
            var emails = TranslatedEmails(messageToPrint.AllEmails);
            Console.WriteLine("Pozvani korisnici: " + string.Join(", ", emails));
            if (accepted)
                Console.WriteLine("Prihvaćen");
            else
                Console.WriteLine("Nije prihvaćen ili nema odgovora");
        }
        public static void PrintAsSender(MessagePrint messageToPrint)
        {

            Console.WriteLine("Naslov: " + messageToPrint.Title);
            var emails = TranslatedEmails(messageToPrint.AllEmails);
            Console.WriteLine("Primatelj(i): " + string.Join(", ", emails));
        }
        public static void PrintUsers(ICollection<UserPrint> usersToPrint)
        {
            var iterator = 0;
            foreach (var user in usersToPrint)
            {
                iterator++;
                var blocked = "";
                if (user.Blocked) { blocked = "(blokiran)"; }
                Console.WriteLine($"{iterator}. {user.Email} {blocked}");
            }
        }
        public static string RandomString(int length)
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
