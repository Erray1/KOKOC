using Ardalis.Result;
using Erray.EntitiesFiltering;
using KOKOC.Matches.Domain.Entities;
using KOKOC.Matches.Domain.Repositories;
using KOKOC.Matches.Infrastructure;
using KOKOC.Matches.Persistence.Contracts.Matches;
using Microsoft.EntityFrameworkCore;

namespace KOKOC.Matches.Application
{
    public class MatchesRepository : IMatchesRepository
    {
        private readonly MatchesDbContext _dbContext;
        public MatchesRepository(MatchesDbContext dbContext)
        {
            _dbContext  = dbContext;
        }
        public async Task<Result<Match>> CreateMatch(string opponentTeamName, string leagueName, string stadiumName, DateTime startingAt)
        {
            var opponentTeam = await _dbContext
                .Teams.SingleOrDefaultAsync(x => x.Name == opponentTeamName);
            if (opponentTeam is null)
            {
                return Result.Conflict($"Команды с названием {opponentTeamName} не существует.\nСначала создайте такую команду");
            }

            var league = await _dbContext
                .Leagues.SingleOrDefaultAsync(x => x.Name == leagueName);
            if (league is null)
            {
                return Result.Conflict($"Лиги с названием {leagueName} не существует.\nСначала создайте такую лигу");
            }

            var stadium = await _dbContext
                .Stadiums.SingleOrDefaultAsync(x => x.Name == stadiumName);
            if (stadium is null)
            {
                return Result.Conflict($"Стадиона с названием {stadiumName} не существует.\nСначала создайте такой стадион");
            }

            if (await MatchExists(opponentTeam.Id, stadium.Id, league.Id, startingAt))
            {
                return Result.Error("Матч с введёнными параметрами уже существует");
            }

            var match = new Match
            {
                Id = Guid.NewGuid(),
                OpponentTeam = opponentTeam,
                League = league,
                Stadium = stadium,
                StartingAt = startingAt,
                IsStarted = DateTime.Now > startingAt
            };
            _dbContext.Matches.Add(match);
            var success = await _dbContext.SaveChangesAsync();
            return success == 1 ? Result.Success() : Result.Error("Ошибка создания матча");
        }


        public async Task<Result> EditMatch(Guid matchId, Match matchChanged)
        {
            var match = await _dbContext.Matches.FindAsync(matchId);
            if (match is null)
            {
                return Result.NotFound($"Не найдено матча с id {matchId}");
            }
            var entry = _dbContext.Matches.Entry(match);
            match = matchChanged;
            var result = await _dbContext.SaveChangesAsync();
            return result == 1 ? Result.Success() : Result.Error("Ошибка обновления");
        }

        public async Task<Result<Match>> GetMatch(Guid matchId)
        {
            var match = await _dbContext.Matches.FindAsync(matchId);
            return match is null ? Result.NotFound() : Result.Success(match);
        }

        public async Task<Result<List<Match>>> GetMatches(FilterRule<Match>[] filters)
        {
            var matches = await _dbContext
                .Matches
                .Filter(filters)
                .ToListAsync();

            return matches switch
            {
                null => Result.NotFound(),
                not null => matches
            };
        }

        private async Task<bool> MatchExists(int opponentId, int stadiumId, int leagueId, DateTime startingAt)
        {
            var existingMatch = await _dbContext
                .Matches
                .SingleOrDefaultAsync(x =>
                x.OpponentTeamId == opponentId
                && x.StadiumId == stadiumId
                && x.LeagueId == leagueId
                && x.StartingAt == startingAt);
            if (existingMatch is null)
            {
                return false;
            }
            return true;
        }
    }
}
