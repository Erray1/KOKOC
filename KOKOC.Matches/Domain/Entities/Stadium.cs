namespace KOKOC.Matches.Domain.Entities
{
    public class Stadium : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
