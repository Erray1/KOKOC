

using KOKOC.Matches.Domain.Entities;

namespace KOKOC.Matches.Domain
{
    public class CalendarData
    {
        public Dictionary<int, Match> MatchesCurrentMonth { get; set; }
        public Dictionary<int, Match>? MatchesPrevMonth { get; set; }
        public Dictionary<int, Match>? MatchesNextMonth { get; set; }
    }
}
