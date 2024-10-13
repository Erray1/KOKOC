
using Ardalis.Result.AspNetCore;
using KOKOC.Matches.Domain.Entities;
using KOKOC.Matches.Domain.Repositories;
using KOKOC.Matches.Persistence.Contracts.Teams;
using Microsoft.AspNetCore.Mvc;

namespace KOKOC.Matches.Persistence.Endpoints.Teams
{
    public class TeamsEndpoints : IEndpoint
    {
        public void Map(IEndpointRouteBuilder builder)
        {
            builder.MapGet("/api/teams", async (IRepository<Team, int> repo) =>
            {
                var result = await repo.GetAllAsync();
                return result.ToMinimalApiResult();
            });
            builder.MapGet("/api/teams/{teamId}", async (int teamId, IRepository<Team, int> repo) =>
            {
                var result = await repo.GetAsync(teamId);
                if (!result.IsSuccess)
                {
                    return result.ToMinimalApiResult();
                }
                return Results.Ok(TeamDto.FromEntity(result.Value));
            });
            builder.MapPost("/api/teams", async (HttpContext context, IRepository<Team, int> repo) =>
            {
            }).RequireAuthorization(cfg => cfg.RequireRole("Admin"));

            builder.MapPatch("/api/teams/{teamId}", async (
                HttpContext context,
                IRepository<Team, int> repo,
                [FromForm] EditTeamRequest request) =>
            {

            }).RequireAuthorization(cfg => cfg.RequireRole("Admin"));
        }
    }
}
