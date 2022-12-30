using Dmail.Data.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dmail.Data.Entities
{
    public class DmailContext : DbContext 
    {
        public DmailContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Event> Events => Set<Event>();
        public DbSet<Message> Messages => Set<Message>();
        public DbSet<EventsUsers> EventUsers => Set<EventsUsers>();
        public DbSet<MessagesReceivers> MessagesReceivers => Set<MessagesReceivers>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<EventsUsers>()
                .HasKey(eu => new { eu.UserId, eu.EventId });
            modelBuilder.Entity<EventsUsers>()
                .HasOne(u=>u.User)
                .WithMany(u=>u.EventUsers)
                .HasForeignKey(ui=>ui.UserId);
            modelBuilder.Entity<EventsUsers>()
                .HasOne(e => e.Event)
                .WithMany(e => e.EventUsers)
                .HasForeignKey(ei => ei.EventId);
            modelBuilder.Entity<Event>()
                .HasOne(s => s.Sender)
                .WithMany(e => e.Events);
            modelBuilder.Entity<User>()
                .Property(x=>x._password)
                .IsRequired();
            modelBuilder.Entity<Message>()
                .HasOne(s => s.Sender)
                .WithMany(e => e.Messages)
                .HasForeignKey(mi => mi.SenderId);
            modelBuilder.Entity<MessagesReceivers>()
                .HasKey(eu => new { eu.ReceiverId, eu.MessageId });
            modelBuilder.Entity<MessagesReceivers>()
                .HasOne(u => u.Message)
                .WithMany(u => u.MessagesReceivers)
                .HasForeignKey(ui => ui.MessageId);
            modelBuilder.Entity<MessagesReceivers>()
                .HasOne(e => e.Receiver)
                .WithMany(e => e.MessagesReceivers)
                .HasForeignKey(ei => ei.ReceiverId);






            base.OnModelCreating(modelBuilder);
        }
    }
    public class DmailDbContextFactory : IDesignTimeDbContextFactory<DmailContext>
    {
        public DmailContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddXmlFile("App.config")
                .Build();

            config.Providers
                .First()
                .TryGet("connectionStrings:add:Dmail:connectionString", out var connectionString);

            var options = new DbContextOptionsBuilder<DmailContext>()
                .UseNpgsql(connectionString)
                .Options;

            return new DmailContext(options);
        }
    }
}
