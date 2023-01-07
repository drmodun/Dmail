using Dmail.Data.Entities.Models;
using Dmail.Domain.Enums;
using Dmail.Domain.Models;
using Dmail.Domain.Repositories;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Dmail.Presentation.Actions
{
    public class AccounActions
    {
        public bool CreateAccount(UserRepo createRepo, string email, string password)
        {
            var check = createRepo.CreateNewUser(email, password);
            if (check == ResponseType.Exists)
            {
                Console.WriteLine("Račun s tim emailom već postoji");
                Console.ReadLine();
                return false;
            }
            Console.WriteLine($"Uspješno napravljen račun email: {email}");
            Console.WriteLine("Pretisnite bilo koji butun za nastavak");
            Console.ReadLine();
            Info.UserId = createRepo.GetIdByEmail(email);
            Info.UserEmail = email;
            return true;
        }
        public DateTime AuthAccount(UserRepo authRepo, string email, string password)
        {
            var failedAttempt = DateTime.MinValue;
            var response = authRepo.Auth(email, password);
            if (response == ResponseType.NotFound)
            {
                Console.WriteLine("Nije pronađen račun povezan na taj email");
                failedAttempt = DateTime.Now;
                Console.ReadLine();
                return failedAttempt;
            }
            else if (response == ResponseType.ValidationFailed)
            {
                Console.WriteLine("Nije upisana točna šifra");
                failedAttempt = DateTime.Now;
                Console.ReadLine();
                return failedAttempt;
            }
            //Add known account when I add account actions
            Console.WriteLine("Uspješno logirani u račun");
            Console.ReadLine();
            Info.UserId = authRepo.GetIdByEmail(email);
            Info.UserEmail = authRepo.GetUser(Info.UserId).Email;
            return DateTime.MinValue;
        }
        public ResponseType CreateNewSpamConnection(int senderId)
        {
            var check = Info.Repos.SpamRepo.TryAdd(Info.UserId, senderId);
            return check;
        }
        public string GetUserEmail(int userId)
        {
            var user = Info.Repos.UserRepo.GetUser(userId).Email;
            return user;
        }
        public int GetUserIdByEmail(string email, UserRepo userRepo)
        {
            var id = userRepo.GetIdByEmail(email);
            return id;
        } 
    }
}
