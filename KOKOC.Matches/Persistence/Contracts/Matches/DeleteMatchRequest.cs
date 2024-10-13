namespace KOKOC.Matches.Persistence.Contracts.Matches
{
    public class DeleteMatchRequest
    {
        public Guid MatchId { get; set; }
        public bool Permanent { get; set; }
    }
}
