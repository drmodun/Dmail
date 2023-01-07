using Dmail.Domain.Factories;
using Dmail.Domain.Repositories;

namespace Dmail.Presentation.Factories
{
    public class RepoMaker
    {
        public static Repos Create()
        {
            var repos = new Repos()
            {
                MessageReceiversRepo = RepositoryFactory.Create<MessageReceiversRepo>(),
                MessageRepo = RepositoryFactory.Create<MessageRepo>(),
                UserRepo = RepositoryFactory.Create<UserRepo>(),
                SpamRepo = RepositoryFactory.Create<SpamRepo>(),
            };
            return repos;

        }
    }
}
