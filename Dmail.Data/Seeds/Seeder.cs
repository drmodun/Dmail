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
                    CreatedAt=new DateTime(2020, 9, 1).ToUniversalTime(),
                    IsEvent=false,
                },
                new Message()
                {
                    Id = 2,
                    SenderId=3,
                    Title="Kupovina",
                    Body="E mos mi kupit miljeko zaboravia san",
                    CreatedAt=new DateTime(2022, 9, 1).ToUniversalTime(),
                    IsEvent=false,
                },
                new Message()
                {
                    Id = 3,
                    SenderId=8,
                    Title="Nagrada",
                    Body="Čestitamo osvojili ste besplatni Iphone 14 da prmiite nagradu samo nam dajte vaš matični broj, oib, pin kartice, sve brojeve vezane uz karticu, adresu, legalno ime...",
                    CreatedAt=new DateTime(2021, 3, 24).ToUniversalTime(),
                    IsEvent=false,
                },
                new Message()
                {
                    Id = 4,
                    SenderId=5,
                    Title="JanVsJan",
                    CreatedAt=new DateTime(2021, 9, 1).ToUniversalTime(),
                    IsEvent=true,
                    Body="",
                    DateOfEvent=new DateTime(2023, 1, 31).ToUniversalTime()
                },
                new Message()
                {
                    Id = 5,
                    SenderId=2,
                    Title="Dump predavanje 8",
                    Body="",
                    CreatedAt=new DateTime(2023, 1, 2).ToUniversalTime(),
                    IsEvent=true,
                    DateOfEvent=new DateTime(2023, 1, 14, 19, 0, 0).ToUniversalTime()
                },
                new Message()
                {
                    Id = 6,
                    SenderId=7,
                    Title = "Job application",
                    CreatedAt=new DateTime(2022, 12, 12).ToUniversalTime(),
                    IsEvent=false,
                    Body="Hello I would like to apply to dump internship, I will also send you my resume",
                },
                new Message()
                {
                    Id = 7,
                    SenderId=6,
                    Title="Resume",
                    Body="Resume: I have succesfully opened visual studio once",
                    CreatedAt=new DateTime(2022, 12, 13).ToUniversalTime(),
                    IsEvent=false,
                },
                new Message()
                {
                    Id = 8,
                    SenderId=4,
                    Title="Job Interview",
                    Body="",
                    DateOfEvent= new DateTime(2023, 1, 13).ToUniversalTime(),
                    CreatedAt=new DateTime(2020, 9, 1).ToUniversalTime(),
                    IsEvent=false,
                },
                new Message()
                {
                    Id = 9,
                    SenderId=4,
                    Title="Obavijest o kicku",
                    Body="S obziron na pad kvalitete tvohij domaćih Jane, moram te nažalost obavijestiti da smo došli do odluke da te izbacimo s dump internshipa. Možeš još pratiti predavanja ali nećeš moći sudjelovati u Ic cupu i više ti se neće moći pregledavati domaći.",
                    CreatedAt=new DateTime(2022, 12, 30).ToUniversalTime(),
                    IsEvent=false,
                },
                new Message()
                {
                    Id = 10,
                    SenderId=10,
                    Title="Help",
                    Body="Wow can you send emails to yourself thats cool",
                    CreatedAt=new DateTime(2023, 1, 2).ToUniversalTime(),
                    IsEvent=false,
                },
            });
            modelBuilder.Entity<MessagesReceivers>().HasData(new List<MessagesReceivers>
            {
                new MessagesReceivers()
                {
                    MessageId= 1,
                    ReceiverId=2,
                    Read=true
                },
                new MessagesReceivers()
                {
                    MessageId= 2,
                    ReceiverId=5,
                    Read=false
                },
                new MessagesReceivers()
                {
                    MessageId= 3,
                    ReceiverId=7,
                    Read=true
                },
                new MessagesReceivers()
                {
                    MessageId= 3,
                    ReceiverId=1,
                    Read=true
                },
                new MessagesReceivers()
                {
                    MessageId= 3,
                    ReceiverId=2,
                    Read=true
                },
                new MessagesReceivers()
                {
                    MessageId= 4,
                    ReceiverId=1,
                    Read=true,
                    Accepted=true
                },
                new MessagesReceivers()
                {
                    MessageId= 4,
                    ReceiverId=2,
                    Read=true,
                    Accepted=false
                },
                new MessagesReceivers()
                {
                    MessageId= 4,
                    ReceiverId=4,
                    Read=false, 
                    Accepted=default(bool)
                },
                new MessagesReceivers()
                {
                    MessageId= 5,
                    ReceiverId=5,
                    Read=true,
                    Accepted=true
                },
                new MessagesReceivers()
                {
                    MessageId= 5,
                    ReceiverId=4,
                    Read=true,
                    Accepted=true
                },
                new MessagesReceivers()
                {
                    MessageId= 5,
                    ReceiverId=6,
                    Read=true
                },
                new MessagesReceivers()
                {
                    MessageId= 6,
                    ReceiverId=2,
                    Read=false
                },
                new MessagesReceivers()
                {
                    MessageId= 6,
                    ReceiverId=4,
                    Read=true
                },
                new MessagesReceivers()
                {
                    MessageId= 7,
                    ReceiverId=7,
                    Read=false
                },
                new MessagesReceivers()
                {
                    MessageId= 7,
                    ReceiverId=4,
                    Read=true
                },
                new MessagesReceivers()
                {
                    MessageId= 8,
                    ReceiverId=7,
                    Read=false
                },
                new MessagesReceivers()
                {
                    MessageId= 9,
                    ReceiverId=1,
                    Read=true
                },
                new MessagesReceivers()
                {
                    MessageId= 10,
                    ReceiverId=10,
                    Read=false
                },
            });
            modelBuilder.Entity<Spam>().HasData(new List<Spam>
            {
                new Spam()
                {
                    BlockerId=2,
                    Blocked=8
                },
                new Spam()
                {
                    BlockerId=4,
                    Blocked=8
                },
                new Spam()
                {
                    BlockerId=2,
                    Blocked=7
                },
                new Spam()
                {
                    BlockerId=1,
                    Blocked=5
                },
                new Spam()
                {
                    BlockerId=2,
                    Blocked=5
                },
                new Spam()
                {
                    BlockerId=4,
                    Blocked=5
                },
                new Spam()
                {
                    BlockerId=2,
                    Blocked=1
                },
                new Spam()
                {
                    BlockerId=7,
                    Blocked=8
                },
                new Spam()
                {
                    BlockerId=6,
                    Blocked=2
                },
                new Spam()
                {
                    BlockerId=5,
                    Blocked=2
                },
            });
        }
    }
}
