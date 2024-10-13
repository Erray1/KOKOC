
using Ardalis.Result.AspNetCore;
using Erray.EntitiesFiltering;
using KOKOC.Matches.Domain.Entities;
using KOKOC.Matches.Domain.Repositories;
using KOKOC.Matches.Persistence.Contracts.Matches;
using Microsoft.AspNetCore.Mvc;

namespace KOKOC.Matches.Persistence.Endpoints.Matches
{
    public class GetMatchesEndoint : IEndpoint
    {
        public void Map(IEndpointRouteBuilder builder)
        {
            builder.MapGet("/api/matches", async (
                [FromQuery] string? filters,
                [FromServices] IMatchesRepository repo) =>
            {
                var filtersCollection = string.IsNullOrEmpty(filters) ?
                Array.Empty<FilterRule<Match>>() : 
                FilterRule<Match>.FromString(filters!);

                var result = await repo.GetMatches(filtersCollection!);
                if (!result.IsSuccess)
                {
                    return result.ToMinimalApiResult();
                }
                return Results.Ok(result.Value.Select(x => MatchCardDto.FromEntity(x)).ToList());
            });
        }
    }
}
