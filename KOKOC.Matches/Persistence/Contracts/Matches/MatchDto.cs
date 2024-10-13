using KOKOC.Matches.Domain.Entities;
using KOKOC.Matches.Domain.ValueTypes;

namespace KOKOC.Matches.Persistence.Contracts.Matches
{
    public class MatchDto
    {
        public Guid Id { get; set; }
        public string OpponentTeamName { get; set; }
        public int OpponentTeamId { get; set; }
        public string StadiumName { get; set; }
        public DateTime StartingAt { get; set; }
        public bool IsStarted { get; set; }
        public MatchScore? Score { get; set; }
        public static MatchDto FromEntity(Match match)
        {
            var dto = new MatchDto
            {
                Id = match.Id,
                IsStarted = match.IsStarted,
                OpponentTeamId = match.OpponentTeamId,
                Score = match.Score,
                StadiumName = match.Stadium.Name,
                StartingAt = match.StartingAt,
                OpponentTeamName = match.OpponentTeam.Name
            };
            return dto;
        }
    }
}
