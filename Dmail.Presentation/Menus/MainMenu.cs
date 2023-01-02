using Dmail.Data.Entities;
using Dmail.Domain.Enums;
using Dmail.Domain.Repositories;

namespace Dmail.Presentation.Menus
{
    public static class MainMenu
    {
        public static int _choice;
        public static UserRepo userRepo;
        public static void Content()
        {
            while (true)
            {
                _choice = -1; 
                Console.Clear();
                Console.WriteLine("Upišite željenu opciju");
                Console.WriteLine("1 - Ulazna pošta");
                Console.WriteLine("2 - Izlazna pošta");
                Console.WriteLine("3 - Spam");
                Console.WriteLine("4 - Pošalji novi mail");
                Console.WriteLine("5 - Pošalji novi događaj");
                Console.WriteLine("6 - Postavke profila");
                Console.WriteLine("7 - Odjava iz profila");
                int.TryParse(Console.ReadLine(), out _choice);
                if (_choice < 0 || _choice>7)
                {
                    Console.WriteLine("Upisan netočan input");
                    Console.ReadLine();
                    continue;
                }
                switch (_choice)
                {
                    case 0:
                        Environment.Exit(0);
                        break;
                    case 1:
                        IncomingMessageMenu.Content();
                        break;
                    case 2:
                        SpamMenu.Content();
                        break ;
                    case 3:
                        NewMessageMenu.Content();
                        break;
                    case 7:
                        return;

                }

            }
        }
        public static bool ConfirmationDialog()
        {
            int confrimation = 0;
            Console.WriteLine("Ova akcija će trajno promijeniti podatke aplikacije, jeste li sigurni da ju želite napraviti");
            Console.WriteLine("1 - Da");
            Console.WriteLine("0 - Ne");
            int.TryParse(Console.ReadLine(), out confrimation);
            if (confrimation == 1)
                return true;
            return false;
        }
        public static void AuthMenu()
        {
            var authRepo = userRepo;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Autentikacija");
                Console.WriteLine("Upišite email i šifru vešeg računa");
                Console.WriteLine("Email: ");
                var email = Console.ReadLine();
                Console.WriteLine("Šifra: ");
                var password = Console.ReadLine();
                var response = authRepo.Auth(email, password);
                if (response == ResponseType.NotFound)
                {
                    Console.WriteLine("Nije pronađen račun povezan na taj email");
                    Console.ReadLine();
                    continue;
                }
                else if (response == ResponseType.ValidationFailed)
                {
                    Console.WriteLine("Nije upisana točna šifra");
                    Console.ReadLine();
                    continue;
                }
                //Add known account when I add account actions
                Content();
                break;

            }
        }
    }
}