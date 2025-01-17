﻿// <auto-generated />
using System;
using Dmail.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Dmail.Data.Migrations
{
    [DbContext(typeof(DmailContext))]
    [Migration("20230106112914_Seed3")]
    partial class Seed3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Dmail.Data.Entities.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateOfEvent")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsEvent")
                        .HasColumnType("boolean");

                    b.Property<int>("SenderId")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("SenderId");

                    b.ToTable("Messages");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Body = "Pomoc pls",
                            CreatedAt = new DateTime(2020, 8, 31, 22, 0, 0, 0, DateTimeKind.Utc),
                            DateOfEvent = new DateTime(2023, 1, 6, 11, 29, 14, 87, DateTimeKind.Utc).AddTicks(5359),
                            IsEvent = false,
                            SenderId = 1,
                            Title = "Pomoc"
                        },
                        new
                        {
                            Id = 2,
                            Body = "E mos mi kupit miljeko zaboravia san",
                            CreatedAt = new DateTime(2022, 8, 31, 22, 0, 0, 0, DateTimeKind.Utc),
                            DateOfEvent = new DateTime(2023, 1, 6, 11, 29, 14, 87, DateTimeKind.Utc).AddTicks(5394),
                            IsEvent = false,
                            SenderId = 3,
                            Title = "Kupovina"
                        },
                        new
                        {
                            Id = 3,
                            Body = "Čestitamo osvojili ste besplatni Iphone 14 da prmiite nagradu samo nam dajte vaš matični broj, oib, pin kartice, sve brojeve vezane uz karticu, adresu, legalno ime...",
                            CreatedAt = new DateTime(2021, 3, 23, 23, 0, 0, 0, DateTimeKind.Utc),
                            DateOfEvent = new DateTime(2023, 1, 6, 11, 29, 14, 87, DateTimeKind.Utc).AddTicks(5398),
                            IsEvent = false,
                            SenderId = 8,
                            Title = "Nagrada"
                        },
                        new
                        {
                            Id = 4,
                            Body = "",
                            CreatedAt = new DateTime(2021, 8, 31, 22, 0, 0, 0, DateTimeKind.Utc),
                            DateOfEvent = new DateTime(2023, 1, 30, 23, 0, 0, 0, DateTimeKind.Utc),
                            IsEvent = true,
                            SenderId = 5,
                            Title = "JanVsJan"
                        },
                        new
                        {
                            Id = 5,
                            Body = "",
                            CreatedAt = new DateTime(2023, 1, 1, 23, 0, 0, 0, DateTimeKind.Utc),
                            DateOfEvent = new DateTime(2023, 1, 14, 18, 0, 0, 0, DateTimeKind.Utc),
                            IsEvent = true,
                            SenderId = 2,
                            Title = "Dump predavanje 8"
                        },
                        new
                        {
                            Id = 6,
                            Body = "Hello I would like to apply to dump internship, I will also send you my resume",
                            CreatedAt = new DateTime(2022, 12, 11, 23, 0, 0, 0, DateTimeKind.Utc),
                            DateOfEvent = new DateTime(2023, 1, 6, 11, 29, 14, 87, DateTimeKind.Utc).AddTicks(5409),
                            IsEvent = false,
                            SenderId = 7,
                            Title = "Job application"
                        },
                        new
                        {
                            Id = 7,
                            Body = "Resume: I have succesfully openned visual studio once",
                            CreatedAt = new DateTime(2022, 12, 12, 23, 0, 0, 0, DateTimeKind.Utc),
                            DateOfEvent = new DateTime(2023, 1, 6, 11, 29, 14, 87, DateTimeKind.Utc).AddTicks(5412),
                            IsEvent = false,
                            SenderId = 6,
                            Title = "Resume"
                        },
                        new
                        {
                            Id = 8,
                            Body = "",
                            CreatedAt = new DateTime(2020, 8, 31, 22, 0, 0, 0, DateTimeKind.Utc),
                            DateOfEvent = new DateTime(2023, 1, 12, 23, 0, 0, 0, DateTimeKind.Utc),
                            IsEvent = false,
                            SenderId = 4,
                            Title = "Job Interview"
                        },
                        new
                        {
                            Id = 9,
                            Body = "S obziron na pad kvalitete tvohij domaćih Jane, moram te nažalost obavijestiti da smo došli do odluke da te izbacimo s dump internshipa. Možeš još pratiti predavanja ali nećeš moći sudjelovati u Ic cupu i više ti se neće moći pregledavati domaći.",
                            CreatedAt = new DateTime(2022, 12, 29, 23, 0, 0, 0, DateTimeKind.Utc),
                            DateOfEvent = new DateTime(2023, 1, 6, 11, 29, 14, 87, DateTimeKind.Utc).AddTicks(5418),
                            IsEvent = false,
                            SenderId = 4,
                            Title = "Obavijest o kicku"
                        },
                        new
                        {
                            Id = 10,
                            Body = "Wow can you send emails to yourself thats cool",
                            CreatedAt = new DateTime(2023, 1, 1, 23, 0, 0, 0, DateTimeKind.Utc),
                            DateOfEvent = new DateTime(2023, 1, 6, 11, 29, 14, 87, DateTimeKind.Utc).AddTicks(5421),
                            IsEvent = false,
                            SenderId = 10,
                            Title = "Help"
                        });
                });

            modelBuilder.Entity("Dmail.Data.Entities.Models.MessagesReceivers", b =>
                {
                    b.Property<int>("ReceiverId")
                        .HasColumnType("integer");

                    b.Property<int>("MessageId")
                        .HasColumnType("integer");

                    b.Property<bool>("Accepted")
                        .HasColumnType("boolean");

                    b.Property<bool>("Read")
                        .HasColumnType("boolean");

                    b.HasKey("ReceiverId", "MessageId");

                    b.HasIndex("MessageId");

                    b.ToTable("MessagesReceivers");
                });

            modelBuilder.Entity("Dmail.Data.Entities.Models.Spam", b =>
                {
                    b.Property<int>("BlockerId")
                        .HasColumnType("integer");

                    b.Property<int>("Blocked")
                        .HasColumnType("integer");

                    b.HasKey("BlockerId", "Blocked");

                    b.ToTable("Spam");
                });

            modelBuilder.Entity("Dmail.Data.Entities.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("_password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "Jan@gmail.com",
                            _password = "janv2"
                        },
                        new
                        {
                            Id = 2,
                            Email = "bartol@dump.hr",
                            _password = "bartolV10"
                        },
                        new
                        {
                            Id = 3,
                            Email = "Marko@markovi.markic",
                            _password = "marko"
                        },
                        new
                        {
                            Id = 4,
                            Email = "Duje@dump.hr",
                            _password = "Kick"
                        },
                        new
                        {
                            Id = 5,
                            Email = "Janko@gmail.com",
                            _password = "janv1"
                        },
                        new
                        {
                            Id = 6,
                            Email = "bart@dump.hr",
                            _password = "bartV10"
                        },
                        new
                        {
                            Id = 7,
                            Email = "Mao@yahoo.com",
                            _password = "mao"
                        },
                        new
                        {
                            Id = 8,
                            Email = "Fake@fakeemail.fakecountry",
                            _password = "Fake"
                        },
                        new
                        {
                            Id = 9,
                            Email = "Empty@empty.empty",
                            _password = "Empty"
                        },
                        new
                        {
                            Id = 10,
                            Email = "User@adress.domain",
                            _password = "Password"
                        });
                });

            modelBuilder.Entity("Dmail.Data.Entities.Models.Message", b =>
                {
                    b.HasOne("Dmail.Data.Entities.Models.User", "Sender")
                        .WithMany("Messages")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("Dmail.Data.Entities.Models.MessagesReceivers", b =>
                {
                    b.HasOne("Dmail.Data.Entities.Models.Message", "Message")
                        .WithMany("MessagesReceivers")
                        .HasForeignKey("MessageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dmail.Data.Entities.Models.User", "Receiver")
                        .WithMany("MessagesReceivers")
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Message");

                    b.Navigation("Receiver");
                });

            modelBuilder.Entity("Dmail.Data.Entities.Models.Spam", b =>
                {
                    b.HasOne("Dmail.Data.Entities.Models.User", "Blocker")
                        .WithMany("Spams")
                        .HasForeignKey("BlockerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Blocker");
                });

            modelBuilder.Entity("Dmail.Data.Entities.Models.Message", b =>
                {
                    b.Navigation("MessagesReceivers");
                });

            modelBuilder.Entity("Dmail.Data.Entities.Models.User", b =>
                {
                    b.Navigation("Messages");

                    b.Navigation("MessagesReceivers");

                    b.Navigation("Spams");
                });
#pragma warning restore 612, 618
        }
    }
}
