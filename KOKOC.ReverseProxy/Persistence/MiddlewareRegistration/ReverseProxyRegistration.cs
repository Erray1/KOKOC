namespace KOKOC.ReverseProxy.Persistence.MiddlewareRegistration
{
    public static class ReverseProxyRegistration
    {
        public static IServiceCollection RegisterReverseProxy(this IServiceCollection services)
        {
            services.AddReverseProxy();
            return services;
        }
    }
}
