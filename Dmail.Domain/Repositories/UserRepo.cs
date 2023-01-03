using Dmail.Data.Entities;
using Dmail.Data.Entities.Models;
using Dmail.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dmail.Domain.Repositories
{
    public class UserRepo : BaseRepo
    {
        public UserRepo(DmailContext dbContext) : base(dbContext)
        {

        }

        public ResponseType Add(User user)
        {
            DbContext.Users.Add(user);
            return SaveChanges();
        }
        public ResponseType Delete(int id)
        {
            var userToDelete = DbContext.Users.Where(u => u.Id == id).FirstOrDefault();
            if (userToDelete == null)
                return ResponseType.NotFound;
            DbContext.Users.Remove(userToDelete);
            return SaveChanges();
        }
        public ResponseType UpdateMail(int id, string email)
        {
            var userToUpdate = DbContext.Users.Find(id);
            if (userToUpdate == null)
                return ResponseType.NotFound;
            if (userToUpdate.Email == email)
                return ResponseType.NotChanged;
            userToUpdate.Email = email;
            return SaveChanges();
        }
        public ResponseType UpdatePassword(int id, string password)
        {
            var userToUpdate = DbContext.Users.Find(id);
            if (userToUpdate == null)
                return ResponseType.NotFound;
            if (userToUpdate._password == password)
                return ResponseType.NotChanged;
            userToUpdate._password = password;
            return SaveChanges();
        }
        public ResponseType AddSpammer(int id, int spamId)
        {
            var connectionMaker = new SpamRepo(DbContext);
            var spam = new Spam()
            {
                BlockerId = id,
                Blocker = DbContext.Users.Where(u => u.Id == spamId).FirstOrDefault(),
                Blocked = spamId,
                IsBlocked = true
            };
            var check = connectionMaker.Add(spam);
            if (check == ResponseType.NotFound)
                return ResponseType.ValidationFailed;
            var blocker = DbContext.Users.Find(id);
            var spammer = DbContext.Spam.Find(spamId);
            if (spammer != null)
                return ResponseType.Exists;
            blocker.Spams.Add(spammer);
            return SaveChanges();
        }
        public ResponseType Auth(string email, string password)
        {
            var account = DbContext.Users.FirstOrDefault(x => x.Email == email);
            if (account == null)
                return ResponseType.NotFound;
            if (account._password != password)
                return ResponseType.ValidationFailed;
            return ResponseType.Success;
        }
        public User? GetUser(int id) => DbContext.Users.Find(id);
        public ICollection<User> AllUsers() => DbContext.Users.ToList();
        public ResponseType CreateNewUser(string email, string password)
        {
            if (DbContext.Users.FirstOrDefault(x => x.Email == email) != null)
                return ResponseType.Exists;
            var user = new User(email, password) {
                Id = DbContext.Users.OrderBy(x => x.Id).Last().Id + 1
            };
            Add(user);
            return ResponseType.Success;
        }

        public int GetIdByEmail(string email){
            var checkEmail = DbContext.Users.FirstOrDefault(x => x.Email == email);
            if (checkEmail == null)
                return -1;
            return checkEmail.Id;}
    }
}
