using Dmail.Presentation.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dmail.Presentation
{
    public static class Info
    {
        public static int UserId;
        public static string UserEmail;
        public static Repos Repos = RepoMaker.Create();
    }
}
