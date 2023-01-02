using Dmail.Domain.Repositories;


namespace Dmail.Domain.Factories;

public class RepositoryFactory
{
    public static TRepository Create<TRepository>()
        where TRepository : BaseRepo
    {
        var dbContext = DmailDbContextFactory.GetDmailContext();
        var repositoryInstance = Activator.CreateInstance(typeof(TRepository), dbContext) as TRepository;
        return repositoryInstance!;
    }
}
