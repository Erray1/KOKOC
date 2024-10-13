namespace KOKOC.Matches.Persistence.Contracts.Teams
{
    public class CreateTeamRequest
    {
        public string TeamName { get; set; }
        public IFormFile Image { get; set; }
    }
}
