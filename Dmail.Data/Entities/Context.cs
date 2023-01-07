using Dmail.Data.Entities.Models;
using Dmail.Data.Enums;
using Dmail.Data.Seeds;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Dmail.Data.Entities
{
    public class DmailContext : DbContext
    {
        public DmailContext(DbContextOptions options) : base(options)
        {
            NpgsqlConnection.GlobalTypeMapper.MapEnum<EventAnswer>();
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Message> Messages => Set<Message>();
        public DbSet<MessagesReceivers> MessagesReceivers => Set<MessagesReceivers>();
        public DbSet<Spam> Spam => Set<Spam>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresEnum<EventAnswer>();
            modelBuilder.Entity<User>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<User>()
                .Property(x => x._password)
                .IsRequired();
            modelBuilder.Entity<Message>()
                .HasOne(s => s.Sender)
                .WithMany(e => e.Messages)
                .HasForeignKey(mi => mi.SenderId);
            modelBuilder.Entity<Message>()
                .Property(x => x.IsEvent);
            modelBuilder.Entity<Message>()
                    .Property(x => x.DateOfEvent);
            modelBuilder.Entity<MessagesReceivers>()
                .HasKey(eu => new { eu.ReceiverId, eu.MessageId });
            modelBuilder.Entity<MessagesReceivers>()
                .Property(x => x.Read);
            modelBuilder.Entity<MessagesReceivers>()
                .HasOne(u => u.Message)
                .WithMany(u => u.MessagesReceivers)
                .HasForeignKey(ui => ui.MessageId);
            modelBuilder.Entity<MessagesReceivers>()
                .HasOne(e => e.Receiver)
                .WithMany(e => e.MessagesReceivers)
                .HasForeignKey(ei => ei.ReceiverId);
            modelBuilder.Entity<MessagesReceivers>()
                .Property(x => x.Answer).HasDefaultValue(EventAnswer.None);
            modelBuilder.Entity<Spam>()
                .HasKey(bu => new { bu.BlockerId, bu.Blocked });
            modelBuilder.Entity<Spam>()
                .HasOne(u => u.Blocker)
                .WithMany(u => u.Spams)
                .HasForeignKey(ui => ui.BlockerId);


            Seeder.Seed(modelBuilder);

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
