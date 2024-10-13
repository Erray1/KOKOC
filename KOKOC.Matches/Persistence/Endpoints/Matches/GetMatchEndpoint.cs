
using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using KOKOC.Matches.Domain.Repositories;
using KOKOC.Matches.Persistence.Contracts.Matches;
using Microsoft.AspNetCore.Mvc;

namespace KOKOC.Matches.Persistence.Endpoints.Matches
{
    public class GetMatchEndpoint : IEndpoint
    {
        public void Map(IEndpointRouteBuilder builder)
        {
            builder.MapGet("/api/matches/{matchId}", async (
                [FromRoute] Guid matchId, 
                [FromServices] IMatchesRepository repo) =>
            {
                var result = await repo.GetMatch(matchId);
                if (!result.IsSuccess)
                {
                    return result.ToMinimalApiResult();
                }
                return Results.Ok(MatchDto.FromEntity(result.Value));
            });
        }
    }
}
