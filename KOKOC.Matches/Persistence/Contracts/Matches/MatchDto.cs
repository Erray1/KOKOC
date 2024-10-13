using KOKOC.Matches.Domain.Entities;

namespace KOKOC.Matches.Persistence.Contracts.Matches
{
    public class MatchDto
    {
        public Guid Id { get; set; }
        public static MatchDto FromEntity(Match match)
        {
            return new MatchDto
            {

            };
        }
    }
}
