using KOKOC.ReverseProxy.Domain;
using System.Text;

namespace KOKOC.ReverseProxy.Application.Identity
{
    public class TelegramLoginValidator : ITelegramLoginValidator
    {
        private readonly IConfiguration _config;
        public TelegramLoginValidator(IConfiguration configuration)
        {
            _config = configuration;
        }
        public bool Validate(IQueryCollection requestQuery)
        {
            var queryParams = requestQuery.ToDictionary(kv => kv.Key, kv => kv.Value.ToString());
            var botToken = _config["Authentication:Telegram:BotToken"]!;

            var checkString = string.Join("\n", queryParams
            .Where(kv => kv.Key != "hash")
            .OrderBy(kv => kv.Key)
            .Select(kv => $"{kv.Key}={kv.Value}"));

            using var hmac = new System.Security.Cryptography.HMACSHA256(Encoding.UTF8.GetBytes(botToken));
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(checkString));
            var computedHash = BitConverter.ToString(hash).Replace("-", "").ToLower();

            return computedHash == queryParams["hash"];
        }
    }
}
