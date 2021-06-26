using maple_syrup_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace maple_syrup_api.Dto
{
    public class GetEventsFromStartDateIn
    {
        public DateTime StartDate { get; set; }
    }
    public class GetEventsFromStartDateOut
    {
        public GetEventsFromStartDateOut()
        {
            EventList = new List<PartialEventGetEventsFromStartDate>();
        }
        public List<PartialEventGetEventsFromStartDate> EventList { get; set; }
    }

    public class PartialEventGetEventsFromStartDate
    {
        public int Id { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public EventType EventType { get; set; }
        public EventStatus EventStatus { get; set; }
    }
}
