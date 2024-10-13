
using Ardalis.Result.AspNetCore;
using KOKOC.Matches.Domain.Entities;
using KOKOC.Matches.Domain.Repositories;
using KOKOC.Matches.Persistence.Contracts.Stadiums;
using Microsoft.AspNetCore.Mvc;

namespace KOKOC.Matches.Persistence.Endpoints.Stadiums
{
    public class StadiumsEndpoints : IEndpoint
    {
        public void Map(IEndpointRouteBuilder builder)
        {
            builder.MapGet("/api/stadiums", async ([FromServices] IRepository<Stadium, int> repo) =>
            {
                var result = await repo.GetAllAsync();
                return result.ToMinimalApiResult();
            });

            builder.MapGet("/api/stadiums/{stadiumId}", async (
                int stadiumId,
                [FromServices] IRepository<Stadium, int> repo) =>
            {
                var result = await repo.GetAsync(stadiumId);
                return result.ToMinimalApiResult();
            });

            builder.MapPost("/api/stadiums", async (
                [FromForm] AddStadiumRequest request,
                [FromServices] IRepository<Stadium, int> repo
                ) =>
            {
                var stadium = new Stadium
                {
                    Name = request.Name,
                };
                var result = await repo.AddAsync(stadium);
                return result.ToMinimalApiResult();
            }).RequireAuthorization(cfg => cfg.RequireRole("Admin"));

            builder.MapPatch("/api/stadiums/{stadiumId}", async (
                int stadiumId,
                [FromForm] EditStadiumRequest request,
                [FromServices] IRepository<Stadium, int> repo) =>
            {
                var source = new Stadium
                {
                    Name = request.Name,
                };
                var result = await repo.EditAsync(stadiumId, source);
                return result.ToMinimalApiResult();
            }).RequireAuthorization(cfg => cfg.RequireRole("Admin"));
        }
    }
}
