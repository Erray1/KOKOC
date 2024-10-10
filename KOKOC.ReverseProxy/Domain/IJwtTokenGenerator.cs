namespace KOKOC.ReverseProxy.Domain
{
    public interface IJwtTokenGenerator
    {
        public string Generate(User user, IList<string> roles);
    }
}
