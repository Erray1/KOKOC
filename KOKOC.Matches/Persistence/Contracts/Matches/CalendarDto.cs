using KOKOC.Matches.Domain;
using KOKOC.Matches.Domain.Entities;

namespace KOKOC.Matches.Persistence.Contracts.Matches
{
    public class CalendarDto
    {
        public Dictionary<int, MatchDto> MatchesCurrentMonth { get; set; }
        public Dictionary<int, MatchDto>? MatchesPrevMonth { get; set; }
        public Dictionary<int, MatchDto>? MatchesNextMonth { get; set; }
        public static CalendarDto FromEntity(CalendarData data)
        {
            var dto = new CalendarDto
            {
                MatchesCurrentMonth = FromEntityDictionary(data.MatchesCurrentMonth)
            };
            if (data.MatchesPrevMonth is not null )
            {
                dto.MatchesPrevMonth = FromEntityDictionary(data.MatchesPrevMonth);
            }
            if (data.MatchesNextMonth is not null)
            {
                dto.MatchesNextMonth = FromEntityDictionary(data.MatchesNextMonth);
            }
            return dto;
        }
        private static Dictionary<int, MatchDto> FromEntityDictionary(Dictionary<int, Match> data)
        {
            return data.ToDictionary(kv =>  kv.Key, kv => MatchDto.FromEntity(kv.Value));
        }
    }
}
