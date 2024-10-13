using System.Reflection;

namespace KOKOC.Matches.Persistence.Endpoints
{
    public static class EndpointsMapper
    {
        public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder builder)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var endpoints = assembly.GetTypes()
                .Where(x => !x.IsInterface && x.IsAssignableTo(typeof(IEndpoint)))
                .Select(x => Activator.CreateInstance(x));
            var mapEndpointMethod = typeof(IEndpoint)
                .GetMethod("Map")!;
            foreach(var endpoint in endpoints)
            {
                mapEndpointMethod.Invoke(endpoint, [builder]);
            }
            return builder;
        }
    }
}
