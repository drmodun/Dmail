using Dmail.Data.Entities;
using Dmail.Domain.Enums;
using Dmail.Domain.Repositories;
namespace Dmail.Presentation.Menus
{
    public static class SpamMenu
    {
        public static int _choice;
        public static SpamRepo? spamRepo;
        public static void Content()
        {
            while (true)
            {
                _choice = -1;
                Console.Clear();
                Console.WriteLine("Spam");
                Console.WriteLine("Izaberite opciju");
                Console.WriteLine("1 - Blokiraj račun");
                Console.WriteLine("2 - Odblokiraj račun");
                int.TryParse(Console.ReadLine(), out _choice);
                if (_choice < 0)
                {
                    Console.WriteLine("Nije upisan valjani input");
                    Console.ReadLine();
                }
                else if (_choice == 0)

                    return;
                else if (_choice == 1)
                    AddSpamConnection(MainMenu.userRepo, spamRepo);
                else if (_choice == 2)

                continue;
            }
        }
        public static void AddSpamConnection(UserRepo userRepo, SpamRepo spamRepo) 
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Upišite ime kojeg računa želite blokirati");
                Console.WriteLine("Upišite 0 za povratak na prijašnji menu");
                var emailToBlock = Console.ReadLine();
                if (emailToBlock.Trim().Length == 0)
                {
                    Console.WriteLine("Nije upisan pravilan email");
                    Console.ReadLine();
                    continue;
                }
                var blocked = userRepo.GetIdByEmail(emailToBlock);
                if (blocked == -1)
                {
                    Console.WriteLine("Taj email ne postoji");
                    Console.ReadLine();
                    continue;
                }
                if (blocked == AccountMenus.UserId)
                {
                    Console.WriteLine("Nije moguće blokirati samog sebe");
                    Console.ReadLine();
                    continue;
                }
                if (emailToBlock == "0")
                    return;
                var confirmation = MainMenu.ConfirmationDialog();
                if (!confirmation)
                    continue;
                var check = spamRepo.TryAdd(AccountMenus.UserId, blocked);
                if (check == ResponseType.ValidationFailed)
                {
                    Console.WriteLine("Nije upisan pravilan email");
                    Console.ReadLine();
                    continue;
                }
                if (check == ResponseType.NotFound)
                {
                    Console.WriteLine("Ne postoji račun s tim emailom");
                    Console.ReadLine();
                    continue;
                }
                Console.WriteLine($"Račun {emailToBlock} blokiran");
                Console.ReadLine();
                return;

            }

        }
        public static void RemoveSpamConnection(UserRepo userRepo, SpamRepo spamRepo)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Odblokiraj račun");
                Console.WriteLine("Upišite email računa kojega želite odblokirati (upišite 0 za povrataak na prijašnji menu)");
                var email = Console.ReadLine();
                if (email == "0")
                    return;
                var blocked = userRepo.GetIdByEmail(email);
                if (blocked == -1)
                {
                    Console.WriteLine("Taj Mail ne postoji");
                    Console.ReadLine();
                    continue;
                }
                var confirmation = MainMenu.ConfirmationDialog();
                if (!confirmation)
                    continue;
                var check = spamRepo.Delete(AccountMenus.UserId, blocked);
                if (check == ResponseType.NotFound)
                {
                    Console.WriteLine("Spam lista ne sadrži tog korisnika");
                    Console.ReadLine();
                    continue;
                }
                Console.WriteLine("Uspješno odblokiran korisnik "+email);
                Console.ReadLine();
                return;
                
                
            }
        }
    }
}
