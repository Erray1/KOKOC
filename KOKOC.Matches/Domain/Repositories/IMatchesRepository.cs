using Ardalis.Result;
using Erray.EntitiesFiltering;
using KOKOC.Matches.Domain.Entities;
using KOKOC.Matches.Persistence.Contracts.Matches;

namespace KOKOC.Matches.Domain.Repositories
{
    public interface IMatchesRepository
    {
        public Task<Result<Match>> CreateMatch(string opponentTeamName, string leagueName, string stadiumName, DateTime startingAt);
        public Task<Result> EditMatch(Guid matchId, Match match);
        public Task<Result<List<Match>>> GetMatches(FilterRule<Match>[] filters);
        public Task<Result<Match>> GetMatch(Guid matchId);
    }
}
