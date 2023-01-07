using Dmail.Domain.Repositories;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dmail.Presentation.Factories
{
    public class Repos
    {
        public MessageRepo MessageRepo;
        public UserRepo UserRepo;
        public MessageReceiversRepo MessageReceiversRepo;
        public SpamRepo SpamRepo;


    }
}
