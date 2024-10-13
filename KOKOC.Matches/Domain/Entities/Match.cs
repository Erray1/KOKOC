using KOKOC.Matches.Domain.ValueTypes;

namespace KOKOC.Matches.Domain.Entities
{
    public sealed class Match : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public int LeagueId { get; set; }
        public League League { get; set; }
        public int OpponentTeamId { get; set; }
        public Team OpponentTeam { get; set; }
        public int StadiumId { get; set; }
        public Stadium Stadium { get; set; }
        public bool IsStarted { get; set; }
        public bool IsEnded { get; set; }
        public MatchScore? Score { get; set; }
        public DateTime StartingAt { get; set; }
    }
}
