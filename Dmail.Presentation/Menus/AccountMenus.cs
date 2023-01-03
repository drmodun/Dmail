using Dmail.Domain.Enums;
using Dmail.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dmail.Presentation.Menus
{
    public static class AccountMenus
    {
        public static int UserId = 0;

        public static void NewAccount(UserRepo userRepo)
        {
            var createRepo = userRepo;

            while (true)
            {
                Console.WriteLine("Novi račun");
                Console.WriteLine("Upišite mail i šifru novog računa (upišite 0 za izlaz na prijašnji menu)");
                Console.WriteLine("Email");
                var email = Console.ReadLine();
                Console.WriteLine("Šifra");
                var password = Console.ReadLine();
                Console.WriteLine("Ponovljena šifra");
                var passCheck = Console.ReadLine();
                if (email == "0")
                    return;
                if (password != passCheck)
                {
                    Console.WriteLine("Šifre se ne podudaraju");
                    Console.ReadLine();
                    continue;
                }
                var confirmation = MainMenu.ConfirmationDialog();
                if (!confirmation)
                    continue;
                if (email.Trim().Length == 0 || password.Trim().Length == 0)
                {
                    Console.WriteLine("Email ili šifra nije upisana u točnom formatu");
                    Console.ReadLine();
                    continue;
                }
                var check = createRepo.CreateNewUser(email, password);
                if (check == ResponseType.Exists)
                {
                    Console.WriteLine("Račun s tim emailom već postoji");
                    Console.ReadLine();
                    continue;
                }
                Console.WriteLine($"Uspješno napravljen račun email: {email}");
                Console.WriteLine("Pretisnite bilo koji butun za nastavak");
                Console.ReadLine();
                UserId=createRepo.GetIdByEmail(email);
                MainMenu.Content();
                return;
            }
        }
        public static void AuthMenu(UserRepo userRepo)
        {
            var authRepo = userRepo;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Autentikacija");
                Console.WriteLine("Upišite email i šifru vešeg računa (upišite 0 za vraćanje na prijašnji menu)");
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
                Console.WriteLine("Uspješno logirani u račun");
                Console.ReadLine();
                UserId= authRepo.GetIdByEmail(email);
                MainMenu.Content();
                break;

            }
        }
    }
}
