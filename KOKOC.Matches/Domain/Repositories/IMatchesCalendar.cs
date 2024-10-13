using Ardalis.Result;

namespace KOKOC.Matches.Domain.Repositories
{
    public interface IMatchesCalendar
    {
        public Task<Result<CalendarData>> GetMatchesForMonth(int currentMonth, int prevRequestedMonth);
    }
}
