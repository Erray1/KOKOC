using KOKOC.Matches.Domain.Entities;

namespace KOKOC.Matches.Persistence.Contracts.Teams
{
    public class TeamDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageURI { get; set; }
        public static TeamDto FromEntity(Team team)
        {
            return new TeamDto
            {
                Id = team.Id,
                Name = team.Name,
                ImageURI = team.ImageURI,
            };
        }
    }
}
