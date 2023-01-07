using Dmail.Domain.Repositories;
using Dmail.Presentation.Actions;


namespace Dmail.Presentation.Menus
{
    public static class AccountMenus
    {
        public static void NewAccount(UserRepo userRepo)
        {
            var createRepo = userRepo;

            while (true)
            {
                Console.WriteLine("Novi račun");
                Console.WriteLine("Upišite mail i šifru novog računa (upišite 0 za izlaz na prijašnji menu)");
                Console.WriteLine("Email");
                var email = Console.ReadLine();
                if (email == "0")
                    return;
                var monkeyPart = email.Split('@');
                if (monkeyPart[0].Length < 1 || monkeyPart.Length != 2)
                {
                    Console.WriteLine("Krivi format emaila");
                    Console.ReadLine();
                    return;
                }
                var dotPart = monkeyPart[1].Split(".");
                if (dotPart[0].Length < 2 || dotPart.Length != 2 || dotPart[1].Length < 3)
                {
                    Console.WriteLine("Krivi format emaila");
                    Console.ReadLine();
                    return;
                }
                Console.WriteLine("Šifra");
                var password = Console.ReadLine();
                if (password == "0")
                    return;
                if (email.Trim().Length == 0 || password.Trim().Length == 0)
                {
                    Console.WriteLine("Email ili šifra nije upisana u točnom formatu");
                    Console.ReadLine();
                    return;
                }
                Console.WriteLine("Ponovljena šifra");
                var passCheck = Console.ReadLine();

                if (password != passCheck)
                {
                    Console.WriteLine("Šifre se ne podudaraju");
                    Console.ReadLine();
                    return;
                }
                var random = Prints.RandomString(10);
                Console.WriteLine(random);
                Console.WriteLine("Upišite broj od gore");
                var captcha = Console.ReadLine();
                if (captcha != random)
                {
                    Console.WriteLine("Nije točno upisana captcha");
                    Console.ReadLine();
                    return;
                }
                var confirmation = MainMenu.ConfirmationDialog();
                if (!confirmation)
                    return;
                var action = new AccounActions();
                var check = action.CreateAccount(createRepo, email, password);
                if (!check)
                    return;
                MainMenu.Content();
                return;
            }
        }
        public static DateTime AuthMenu(UserRepo userRepo, DateTime failedAttempt)
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
                if (email == "0" || password == "0")
                    return DateTime.Now;
                if ((DateTime.Now - failedAttempt).TotalSeconds < 30)
                {
                    Console.WriteLine("Pričekajte 30 sekundi od prošlog neuspjelog pokušaja logina");
                    Console.WriteLine("Preostalo vrijeme: " + (30 - (DateTime.Now - failedAttempt).TotalSeconds).ToString() + " sekundi");
                    Console.ReadLine();
                    continue;
                }
                var auth = new AccounActions();
                var check = auth.AuthAccount(authRepo, email, password);
                if (check != DateTime.MinValue)
                {
                    failedAttempt = check;
                    return failedAttempt;
                }
                MainMenu.Content();
                return failedAttempt;
                break;

            }
        }
    }
}
