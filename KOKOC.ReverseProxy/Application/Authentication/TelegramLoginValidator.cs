using System.Security.Cryptography;
using System.Text;

namespace KOKOC.ReverseProxy.Application.Authentication
{
    public static class TelegramLoginValidator
    {
        public static bool Validate(IQueryCollection query, string botToken)
        {
            string? queryHash = query["hash"];
            if (string.IsNullOrEmpty(queryHash))
            {
                throw new KeyNotFoundException("В запросе нет хэша");
            }

            var formattedQueryParams = query
                .Where(x => x.Key != "hash")
                .OrderBy(x => x.Key)
                .Select(x => $"{x.Key}={x.Value}");
            var checkString = string.Join("\n", formattedQueryParams);

            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(botToken));
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(checkString));
            var computedHash = BitConverter
                .ToString(hash)
                .Replace("-", "")
                .ToLower();

            return computedHash == queryHash;
        }
    }
}
