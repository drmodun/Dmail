using Dmail.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Dmail.Domain.Factories;

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
