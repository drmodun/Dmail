using Dmail.Data.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dmail.Data.Seeds
{
    public static class Seeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new List<User>
            {
                new User("Jan@gmail.com", "janv2")
                { Id = 1},
                new User("bartol@dump.hr", "bartolV10"){Id= 2},
                new User("Marko@markovi.markic", "marko"){Id= 3},
                new User("Duje@dump.hr", "Kick"){Id= 4},
                new User("Janko@gmail.com", "janv1")
                { Id = 5},
                new User("bart@dump.hr", "bartV10"){Id= 6},
                new User("Mao@yahoo.com", "mao"){Id= 7},
                new User("Fake@fakeemail.fakecountry", "Fake"){Id= 8},
                new User("Empty@empty.empty", "Empty"){Id= 9},
                new User("User@adress.domain", "Password") { Id =10}
            });
            modelBuilder.Entity<Message>().HasData(new List<Message>
            {
                new Message()
                {
                    Id = 1,
                    SenderId=1,
                    Title="Pomoc",
                    Body="Pomoc pls",
                    CreatedAt=new DateTime(2020, 9, 1).ToUniversalTime()
                }
            });
        }
    }
}
