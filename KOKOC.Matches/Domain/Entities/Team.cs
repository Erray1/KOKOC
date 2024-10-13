namespace KOKOC.Matches.Domain.Entities
{
    public class Team : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageURI { get; set; }
        
    }
}
