namespace KOKOC.Matches.Persistence.Contracts.Matches
{
    public class CreateMatchRequest
    { 
        public DateTime StartingAt { get; set; }
        public string OpponentTeamName { get; set; }
        public string LeagueName { get; set; }
        public string StadiumName { get; set; }
    }
}
