using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Dmail.Data.Entities;

namespace Dmail.Domain.Factories;

public static class DmailDbContextFactory
{
    public static DmailContext GetDmailContext()
    {
        var options = new DbContextOptionsBuilder()
            .UseNpgsql(ConfigurationManager.ConnectionStrings["Dmail"].ConnectionString)
            .Options;

        return new DmailContext(options);
    }
}
