
using KOKOC.Matches.Domain.Repositories;
using KOKOC.Matches.Persistence.Contracts.Matches;

namespace KOKOC.Matches.Persistence.Endpoints.Matches
{
    //public class DeleteMatchEndpoint : IEndpoint
    //{
    //    public void Map(IEndpointRouteBuilder builder)
    //    {
    //        builder.MapDelete("matches", async (HttpContext context, IMatchesRepository repo) =>
    //        {
    //            var request = await context.Request.ReadFromJsonAsync<DeleteMatchRequest>();
    //            if (request is null)
    //            {
    //                context.Response.StatusCode = StatusCodes.Status400BadRequest;
    //                await context.Response.WriteAsync("Пустой запрос");
    //                return;
    //            }
    //            var result = await repo.DeleteMatch(request);
    //            if (!result.IsSuccess)
    //            {
    //                context.Response.StatusCode = StatusCodes.Status400BadRequest;
    //                await context.Response.WriteAsJsonAsync(new
    //                {
    //                    Description = "Не удалось удалить матч",
    //                    Erros = result.Errors.ToList()
    //                });
    //                return;
    //            }
    //            context.Response.StatusCode = StatusCodes.Status204NoContent;

    //        }).RequireAuthorization(cfg =>
    //        {
    //            cfg.RequireRole("Admin");
    //        });

    //    }
    //}
}
