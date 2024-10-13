
using Ardalis.Result.AspNetCore;
using KOKOC.Matches.Domain.Repositories;
using KOKOC.Matches.Persistence.Contracts.Matches;
using Microsoft.AspNetCore.Mvc;

namespace KOKOC.Matches.Persistence.Endpoints.Matches
{
    public class GetMatchesCalendarEndpoint : IEndpoint
    {
        public void Map(IEndpointRouteBuilder builder)
        {
            builder.MapGet("/api/matches/calendar", async (
                [FromQuery] int? currentMonth, 
                [FromQuery] int? prevMonth,
                [FromServices] IMatchesCalendar calendar) =>
            {
                
                if (!currentMonth.HasValue)
                {
                    return Results.BadRequest(new
                    {
                        Error = "Query не содержит текущего месяца"
                    });
                }
                if (!prevMonth.HasValue) prevMonth = currentMonth.Value;
                var result = await calendar.GetMatchesForMonth(currentMonth.Value, prevMonth.Value);
                if (!result.IsSuccess) return result.ToMinimalApiResult();

                return Results.Ok(CalendarDto.FromEntity(result.Value));
            });
        }
    }
}
