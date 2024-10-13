using Ardalis.Result;
using KOKOC.Matches.Domain;
using KOKOC.Matches.Domain.Repositories;
using KOKOC.Matches.Infrastructure;

namespace KOKOC.Matches.Application
{
    public class MatchesCalendar : IMatchesCalendar
    {
        private readonly MatchesDbContext _dbContext;
        public MatchesCalendar(MatchesDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Result<CalendarData>> GetMatchesForMonth(int currentMonth, int prevRequestedMonth)
        {
            throw new NotImplementedException();
        }
    }
}
