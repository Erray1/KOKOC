
using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using KOKOC.Matches.Domain.Entities;
using KOKOC.Matches.Domain.Repositories;
using KOKOC.Matches.Persistence.Contracts.Matches;
using Microsoft.AspNetCore.Mvc;

namespace KOKOC.Matches.Persistence.Endpoints.Matches
{
    public class EditMatchEndpoint : IEndpoint
    {
        public void Map(IEndpointRouteBuilder builder)
        {
            builder.MapPatch("/api/matches/{matchId}", async (
                [FromRoute] Guid matchId,
                [FromForm] EditMatchRequest request,
                [FromServices] IMatchesRepository repo) =>
            {
                //if (newMatch is null)
                //{
                //    return Results.BadRequest("Ошибка чтения данных");
                //}
                //var result = await repo.EditMatch(matchId, newMatch);
                //return result.ToMinimalApiResult();
                
            }).RequireAuthorization(cfg =>
              {
                cfg.RequireRole("Admin");
              });

            builder.MapPatch("/api/matches/{matchId}/stats", async (
                Guid matchId,
                [FromForm] SetGoalCountRequest request
                ) =>
            {

            }).RequireAuthorization(cfg =>
                {
                    cfg.RequireRole("MatchObserver");
                });
        }
    }
}
