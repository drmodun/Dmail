using Dmail.Presentation.Factories;

namespace Dmail.Presentation
{
    public static class Info
    {
        public static int UserId;
        public static string UserEmail;
        public static Repos Repos = RepoMaker.Create();
    }
}
