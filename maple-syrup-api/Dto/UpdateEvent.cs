using maple_syrup_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace maple_syrup_api.Dto
{
    public class UpdateEventIn
    {
        public int Id { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public EventType EventType { get; set; }
        public EventStatus EventStatus { get; set; }
        public string FightName { get; set; }
    }

    public class UpdateEventOut
    {
        public int Id { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public EventType EventType { get; set; }
        public EventStatus EventStatus { get; set; }
        public string FightName { get; set; }

    }
}
