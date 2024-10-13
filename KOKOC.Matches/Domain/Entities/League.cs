namespace KOKOC.Matches.Domain.Entities
{
    public class League : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
