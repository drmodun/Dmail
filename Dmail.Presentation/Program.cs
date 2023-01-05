// See https://aka.ms/new-console-template for more information
using Dmail.Domain.Factories;
using Dmail.Presentation.Menus;
using Dmail.Domain.Repositories;

MainMenu.userRepo = new UserRepo(DmailDbContextFactory.GetDmailContext());
SpamMenu.spamRepo = new SpamRepo(DmailDbContextFactory.GetDmailContext());
IncomingMessageMenu.messageRepo = new MessageRepo(DmailDbContextFactory.GetDmailContext());
IncomingMessageMenu.messageReceiversRepo = new MessageReceiversRepo(DmailDbContextFactory.GetDmailContext());
Console.WriteLine("Hello, World!");
MainMenu.Accounts();