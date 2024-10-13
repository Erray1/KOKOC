
using KOKOC.Matches.Domain.Repositories;
using KOKOC.Matches.Persistence.Contracts.Matches;
using Microsoft.AspNetCore.Mvc;

namespace KOKOC.Matches.Persistence.Endpoints.Matches
{
    public class CreateMatchEndpoint : IEndpoint
    {
        public void Map(IEndpointRouteBuilder builder)
        {
            builder.MapPost("/api/matches", async (
                [FromForm] CreateMatchRequest request,
                [FromServices] IMatchesRepository repo) =>
            {
                var result = await repo.CreateMatch(
                    request.OpponentTeamName, 
                    request.LeagueName, 
                    request.StadiumName,
                    request.StartingAt);

                if (!result.IsSuccess)
                {
                    return Results.BadRequest(new
                    {
                        Description = "Не удалось создать матч",
                        Erros = result.Errors.ToList()
                    });
                }
                return Results.Ok(new
                {
                    Value = result.Value
                });

            }).RequireAuthorization(cfg =>
            {
                cfg.RequireRole("Admin");
            });
                
        }
    }
}
