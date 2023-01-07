using Dmail.Data.Entities;
using Dmail.Domain.Enums;
using Dmail.Domain.Models;
using Dmail.Domain.Repositories;
namespace Dmail.Presentation.Menus
{
    public static class SpamMenu
    {
        public static int _choice;
        public static void Content(UserPrint user)
        {
            while (true)
            {
                _choice = -1;
                Console.Clear();
                Console.WriteLine("Korisnik "+user.Email.ToString());
                Console.WriteLine("Izaberite opciju");
                Console.WriteLine("1 - Blokiraj račun");
                Console.WriteLine("2 - Odblokiraj račun");
                var choice = Console.ReadLine();
                int.TryParse(choice, out _choice);
                if (_choice == 0 && choice!="0")
                {
                    Console.WriteLine("Nije upisan valjani input");
                    Console.ReadLine();
                }
                switch (_choice)
                {
                    case 0:
                        return;
                    case 2:
                        RemoveSpamConnection(Info.Repos.UserRepo, Info.Repos.SpamRepo, user); 
                        break;
                    case 1:
                        AddSpamConnection(Info.Repos.UserRepo, Info.Repos.SpamRepo, user);
                        break;
                    default:
                        Console.WriteLine("Nije upisan točan input");
                        Console.ReadLine();
                        break;
                }

                continue;
            }
        }
        public static void AddSpamConnection(UserRepo userRepo, SpamRepo spamRepo, UserPrint blocked) 
        {
                if (blocked.Blocked)
                {
                    Console.WriteLine("Nije moguće blokirati već blokirani račun");
                    Console.ReadLine();
                    return;
                }
                var confirmation = MainMenu.ConfirmationDialog();
                if (!confirmation)
                    return;
                var check = spamRepo.TryAdd(Info.UserId, blocked.Id);
                if (check == ResponseType.ValidationFailed)
                {
                    Console.WriteLine("Nije upisan pravilan email");
                    Console.ReadLine();
                    return;
                }
                if (check == ResponseType.NotFound)
                {
                    Console.WriteLine("Ne postoji račun s tim emailom");
                    Console.ReadLine();
                    return;
                }
                Console.WriteLine($"Račun {blocked.Email} blokiran");
                Console.ReadLine();
                return;

            }
        public static void RemoveSpamConnection(UserRepo userRepo, SpamRepo spamRepo, UserPrint unblocked)
        {
                if (!unblocked.Blocked)
                {
                    Console.WriteLine("Nije moguće odblokirati neblokiran račun");
                    Console.ReadLine();
                    return;
                }
                var confirmation = MainMenu.ConfirmationDialog();
                if (!confirmation)
                    return;
                var check = spamRepo.Delete(Info.UserId, unblocked.Id);
                if (check == ResponseType.NotFound)
                {
                    Console.WriteLine("Spam lista ne sadrži tog korisnika");
                    Console.ReadLine();
                    return;
                }
                Console.WriteLine("Uspješno odblokiran korisnik "+unblocked.Email);
                Console.ReadLine();
                return;
                
                
            }
        }
    }
