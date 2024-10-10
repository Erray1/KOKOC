namespace KOKOC.ReverseProxy.Domain.Constants
{
    public static class AppIdentityConstants
    {
        public static class RoleNames
        {
            public const string Admin = "Admin";
            public const string BaseUser = "Base";

        }
        public static class ProviderIDs
        {
            public const string VKontakte = "VK";
            public const string Telegram = "TG";
            public const string Application = "APP";
        }
        public static class UserPhotoUrls
        {
            private static readonly List<string> _urls;
            static UserPhotoUrls()
            {
                List<string> photoNames = [];
            }


            public static string GetRandomPhotoUrl()
            {
                return "";
            }
        }
    }
}
