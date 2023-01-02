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
                new User("bartol@dump.hr", "bartolV10"){Id= 2}
            });
        }
    }
}
