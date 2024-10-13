using System.ComponentModel.DataAnnotations;

namespace KOKOC.Matches.Persistence.Contracts.Teams
{
    public class EditTeamRequest
    {
        public string? Name { get; set; }
        public IFormFile? Image { get; set; }
    }
}
