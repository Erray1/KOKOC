namespace KOKOC.ReverseProxy.Domain
{
    public interface ITelegramLoginValidator
    {
        public bool Validate(IQueryCollection requestQuery);
    }
}
